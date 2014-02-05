using Sphero.Communication;
using Sphero.Core;
using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Devices
{
    /// <summary>
    /// Core virtual device
    /// </summary>
    public class CoreDevice
    {
        #region Fields

        private SpheroConnection _connection;

        #endregion

        #region Constructor

        public CoreDevice(SpheroConnection connection)
        {
            this._connection = connection;
            this._connection.SetCore(this);
        }

        #endregion

        #region Delegates

        public delegate void PowerStateNotification(CoreDevice device, PowerState state);

        #endregion

        #region Events

        /// <summary>
        /// If SetPowerStateNotificationON was called this event will fire every 10 seconds with PowerState.
        /// Call SetPowerStateNotificationOFF to stop the notification
        /// </summary>
        public event PowerStateNotification OnPowerStateNotification;

        #endregion

        #region Public Methods

        /// <summary>
        /// Send a Ping command to Sphero
        /// </summary>
        /// <param name="pingCallback">Callback used to handle the response of the CoreDevice</param>
        public void Ping(Action<bool> pingCallback)
        {
            CommandPacket command = new CommandPacket(0xFF, DeviceIDs.CORE, CommandIDs.Core.PING, null);
            _connection.SendCommand(command, pingResponse =>
                {
                    if(pingCallback != null)
                    {
                        if (pingResponse.Data == null)
                            pingCallback(false);

                        pingCallback((pingResponse.Data[0] & 0x01) == 0x01);
                        
                    }
                });
        }

        /// <summary>
        /// Get some information of sphero bluetooth identification
        /// </summary>
        /// <param name="bluetoothCallback">Callback used to handle the response of the CoreDevice</param>
        public void GetBluetoothInformation(Action<BluetoothInformation> bluetoothCallback)
        {
            CommandPacket command = new CommandPacket(0xFF, DeviceIDs.CORE, CommandIDs.Core.GET_BLUETOOTH_INFO, null);
            _connection.SendCommand(command, btinfoResponse =>
                {
                    if (bluetoothCallback != null)
                    {
                        BluetoothInformation infos = new BluetoothInformation(btinfoResponse.Data);
                        bluetoothCallback(infos);
                    }
                });
        }

        /// <summary>
        /// Get different informaitons about versions of Sphero, like Hardware version, soft version ...
        /// </summary>
        /// <param name="SpheroVersion">Callback used to handle the response of the CoreDevice</param>
        public void GetVersioning(Action<SpheroVersion> spheroVersionCallback)
        {
            CommandPacket command = new CommandPacket(0xFF, DeviceIDs.CORE, CommandIDs.Core.GET_VERSIONING, null);
            _connection.SendCommand(command, response =>
            {
                SpheroVersion versionInformations = new SpheroVersion(response.Data);

                if (spheroVersionCallback != null)
                    spheroVersionCallback(versionInformations);
            });
        }

        /// <summary>
        /// When this method is call, CoreDevice will fire OnPowerStateNotification every 10 seconds with powerstate
        /// </summary>
        public void SetPowerStateNotificationON()
        {
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.CORE, CommandIDs.Core.SET_POWER_NOTIFICATION, new byte[] { 0x01 });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// When this method is call, CoreDevice will stop firing OnPowerStateNotification
        /// </summary>
        public void SetPowerStateNotificationOFF()
        {
            CommandPacket command = new CommandPacket(0xFE, DeviceIDs.CORE, CommandIDs.Core.SET_POWER_NOTIFICATION, new byte[] { 0x00 });
            _connection.SendCommand(command);
        }

        /// <summary>
        /// Change the name of sphero
        /// </summary>
        /// <param name="spheroName">Name wanted</param>
        public void SetDeviceName(string spheroName, Action<bool> callback)
        {
            CommandPacket command = new CommandPacket(0xFF, DeviceIDs.CORE, CommandIDs.Core.SET_DEVICE_NAME, StringHelper.StringToAscii(spheroName));
            _connection.SendCommand(command, response =>
            {
                if (callback != null)
                {
                    if (response.ResponseCode == MessageResponseCode.ORBOTIX_RSP_CODE_OK)
                    {
                        callback(true);
                    }
                    else
                        callback(false);
                }
            });
        }

        #endregion


        #region Internal Methods

        internal void RaisePowerNotification(CoreDevice device, PowerState state)
        {
            if (OnPowerStateNotification != null)
            {
                OnPowerStateNotification(this, state);
            }
        }

        #endregion
    }
}
