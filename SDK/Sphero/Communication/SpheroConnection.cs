using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Networking;
using Windows.Foundation;
using Sphero.Devices;
using Sphero.Core;
using Sphero.Locator;
using System.Threading;

namespace Sphero.Communication
{
    /// <summary>
    /// Handle connection with Sphero and used to send low-level messages
    /// </summary>
    public sealed class SpheroConnection
    {
        #region Fields

        private SpheroInformation _peerInformation;
        private StreamSocket _socket;

        private CoreDevice _core;
        private SpheroDevice _sphero;

        private List<byte> _data = new List<byte>();
        private Dictionary<byte, Action<ResponsePacket>> _responseDictionnary;
        private byte _currentSeq;
        private SemaphoreSlim _syncLock = new SemaphoreSlim(1);

        #endregion

        #region Properties

        /// <summary>
        /// Get/Set if trace in the debug console
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// Name of the device over bluetooth
        /// </summary>
        public string BluetoothName
        {
            get
            {
                if (_peerInformation == null)
                    return null;

                return _peerInformation.DisplayName;
            }
        }
        #endregion

        #region Constructor(s)

        internal SpheroConnection(SpheroInformation peerInformation)
        {
            _peerInformation = peerInformation;
            _responseDictionnary = new Dictionary<byte, Action<ResponsePacket>>();
            _currentSeq = 0x01;
        }

        #endregion

        #region Delegates

        public delegate void Disconnected();

        #endregion

        #region Events

        /// <summary>
        /// This event will fire when 
        /// </summary>
        public event Disconnected OnDisconnection;

        #endregion

        internal void SetCore(CoreDevice core)
        {
            this._core = core;
        }

        internal void SetSphero(SpheroDevice sphero)
        {
            this._sphero = sphero;
        }

      

        internal ResponsePacket ParseResponse(byte[] data)
        {
            ResponsePacket response = null;

            _data.AddRange(data);
            if (_data.Count >= 6)
            {
                if (_data[0] != 255 || _data[1] != 255)
                {
                    // Bad message case
                    if (_data[0] != 255 || _data[1] != 254)
                    {
                        // No Sphero Message
                        // Clean data until the next message
                        int nextMessageIndex = _data.IndexOf(255);
                        if (nextMessageIndex >= 0)
                            _data.RemoveRange(0, nextMessageIndex);
                        else
                            _data.Clear();
                    }

                    // Asynchronous message case

                    byte lengthMSB = _data[3], lengthLSB = _data[4];
                    int length = BitConverter.ToUInt16(new byte[] { lengthLSB, lengthMSB }, 0) + 5;
                    if (_data.Count >= length)
                    {

                        byte[] responseArray = new byte[length];
                        _data.CopyTo(0, responseArray, 0, length);
                        _data.RemoveRange(0, length);

                        AsyncResponsePacket packet = new AsyncResponsePacket(responseArray);
                        DispatchAsyncResponse(packet);
                    }

                }
                else
                {
                    // Acknowledgement
                    int length = 5 + _data[4];
                    if (_data.Count >= length)
                    {
                        byte[] responseArray = new byte[length];
                        _data.CopyTo(0, responseArray, 0, length);
                        _data.RemoveRange(0, length);
                        response = new ResponsePacket(responseArray);
                    }

                }
            }


            return response;
        }

        internal void DispatchAsyncResponse(AsyncResponsePacket response)
        {
           
            switch(response.ResponseCode)
            {
                case AsynchronousId.POWER_NOTIFICATION:
                   
                    if(_core != null)
                    {
                        _core.RaisePowerNotification(_core, (PowerState)response.Data[0]);
                    }

                    break;
                case AsynchronousId.SENSOR_DATA_STREAMING:
                    if (_sphero != null)
                    {
                        SensorData sensorData = new SensorData(_sphero.Mask, _sphero.Mask2, response.Data);

                        _sphero.RaiseSensorDataNotification(sensorData);
                    }
                    break;
                case AsynchronousId.SSB_DATA:
                    string data = Encoding.UTF8.GetString(response.Data, 0, response.Data.Length);
                    Debug.WriteLine("Async SSB message : {0}", BitConverter.ToString(response.ToArray()));

                    break;
                case AsynchronousId.XP_UPDATE:
                    if (_sphero != null)
                    {
                        _sphero.RaiseXpNotification(response.Data[0]);
                    }
                    break;
                case AsynchronousId.BOOST_UPDATE:
                    if (_sphero != null)
                    {
                        _sphero.RaiseBoostNotification(response.Data[0]);
                    }
                    break;
                case AsynchronousId.COLLISION_DETECTED:
                    if (_sphero != null)
                    {
                        _sphero.RaiseCollisionDetected(new Collisions.CollisionData(response.Data));
                    }
                    break;
                default:
                    if (Verbose)
                        Debug.WriteLine("Async message : {0}", BitConverter.ToString(response.ToArray()));
                    break;
            }
        }

        internal void DispatchResponse(ResponsePacket response)
        {
           if(_responseDictionnary.ContainsKey(response.SequenceNumber))
           {
               if (_responseDictionnary[response.SequenceNumber] != null)
                   _responseDictionnary[response.SequenceNumber](response);

               _responseDictionnary.Remove(response.SequenceNumber);
           }
        }

        /// <summary>
        /// Open connection
        /// </summary>
        public async Task<bool> Connect()
        {
            if (_peerInformation == null)
                return false;

            _socket = new StreamSocket();

            try
            {
                
                await _socket.ConnectAsync(_peerInformation.HostName, "1");
                ReadBuffer(16);
                
            }
            catch(Exception ex)
            {
                if (Verbose)
                    Debug.WriteLine("Error while connecting to device : " + ex.Message);

                return false;
            }
            

            return true;
        }

        /// <summary>
        /// Disconnect the device
        /// </summary>
        public async Task Disconnect()
        {
            if (_socket != null)
            {
                await _socket.OutputStream.FlushAsync();
                
                _socket.Dispose();
                _socket = null;

                if (OnDisconnection != null)
                    OnDisconnection();
            }
        }

        /// <summary>
        /// Send a CommandPacket object to the sphero device
        /// </summary>
        /// <param name="packet">Command to send to sphero device</param>
        /// <param name="responseCallback">If a response is needed, the responseCallback will be call with response data</param>
        public async void SendCommand(CommandPacket packet, Action<ResponsePacket> responseCallback = null)
        {
            DataWriter dataWriter = new DataWriter();

            try
            {
                if (packet.NeedResponse)
                {
                    // Create async lock
                    await _syncLock.WaitAsync();

                    packet.SetSequenceNumber(_currentSeq);
                    _responseDictionnary.Add(_currentSeq, responseCallback);
                    _currentSeq++;

                    // Reinit sequence Number
                    if (_currentSeq == 0xFE)
                        _currentSeq = 0x01;

                    // Show message in output window
                    if (Verbose)
                        Debug.WriteLine(BitConverter.ToString(packet.ToArray()));

                    // Release async lock
                    _syncLock.Release();


                }
                else
                    packet.SetSequenceNumber(0);


                // Write de commandpacket array on the socket
                await _socket.OutputStream.WriteAsync(packet.ToArray().AsBuffer());

                await _socket.OutputStream.FlushAsync();
            }
            catch (Exception ex)
            {
                if (Verbose)
                    Debug.WriteLine("Error while sending command : " + ex.Message);
            }
           
        }

        /// <summary>
        /// This method is call when data was readed from the socket input buffer
        /// </summary>
        /// <param name="bytesRead"></param>
        /// <param name="readPacket"></param>
        private void OnDataReadCompletion(UInt32 bytesRead, DataReader readPacket)
        {

            if (readPacket == null)
            {
                if(Verbose)
                    Debug.WriteLine("DataReader is null");
            }
            else if (readPacket.UnconsumedBufferLength != 0)
            {
                Byte[] numArray = new Byte[bytesRead];
                readPacket.ReadBytes(numArray);
                ResponsePacket response = ParseResponse(numArray);

                if (response != null)
                {
                    DispatchResponse(response);

                    if (Verbose)
                        Debug.WriteLine("Nouveau message : {0}", BitConverter.ToString(response.ToArray()));
                }

                ReadBuffer(16);
            }
            else
            {
                if (OnDisconnection != null)
                    OnDisconnection();

                if (Verbose)
                {
                    Debug.WriteLine("Received zero bytes from the socket. Server must have closed the connection.");
                    Debug.WriteLine("Try disconnecting and reconnecting to the server");
                }
            }
        }

        /// <summary>
        /// Read continually the input buffer and parse reponse
        /// </summary>
        /// <param name="length">length to read</param>
        private void ReadBuffer(Int32 length)
        {
            if (_socket == null)
                return;
            try
            {
                var buffer = new Byte[length];
                var asyncOperationWithProgress = _socket.InputStream.ReadAsync(buffer.AsBuffer(), (UInt32)length, InputStreamOptions.Partial);
                asyncOperationWithProgress.Completed += (info, status) =>
                {
                    if (info == null)
                        return;

                    switch (status)
                    {
                        case AsyncStatus.Completed:
                        case AsyncStatus.Error:
                            {
                                try
                                {
                                    IBuffer results = info.GetResults();
                                    OnDataReadCompletion(results.Length, DataReader.FromBuffer(results));
                                }
                                catch (Exception exception)
                                {
                                    if(Verbose)
                                        Debug.WriteLine(String.Concat("Read operation failed:  ", exception.Message));
                                }
                                break;
                            }
                    }
                };
            }
            catch (Exception exception1)
            {
                if(Verbose)
                    Debug.WriteLine(String.Concat("Failed to post a read failed with error:  ", exception1.Message));
            }
        }

    }
}
