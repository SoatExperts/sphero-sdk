using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Communication
{
    /// <summary>
    /// Response packet from a sphero Device
    /// </summary>
    public class ResponsePacket
    {
        private const byte DATA_START_INDEX = 5;

        private byte _SOP1;
        private byte _SOP2;
        private byte _MRSP;
        private byte _SEQ;
        private byte _DLEN;
        private byte[] _data;
        private byte _CHK;

        public MessageResponseCode ResponseCode { get { return (MessageResponseCode)_MRSP; } }
        public byte[] Data { get { return _data; } }

        public byte SequenceNumber { get { return _SEQ; } }


        public ResponsePacket(byte[] responseBuffer)
        {
            if (responseBuffer == null)
                return;

            if (responseBuffer.Length < 6)
                return;

            _SOP1 = responseBuffer[0];
            _SOP2 = responseBuffer[1];
            _MRSP = responseBuffer[2];
            _SEQ = responseBuffer[3];
            _DLEN = responseBuffer[4];

            if(_DLEN > 1)
            {
                _data = new byte[_DLEN-1];
                Array.Copy(responseBuffer, 5, _data, 0, _DLEN-1);
            }

            _CHK = responseBuffer[responseBuffer.Length - 1];
        }

        /// <summary>
        /// Format the packet to array
        /// </summary>
        /// <returns>Formatted packet</returns>
        public byte[] ToArray()
        {
            byte[] packetArray = new byte[_DLEN + 5];
            packetArray[0] = _SOP1;
            packetArray[1] = _SOP2;
            packetArray[2] = _MRSP;
            packetArray[3] = _SEQ;
            packetArray[4] = _DLEN;

            if (_data != null)
            {
                Array.Copy(_data, 0, packetArray, DATA_START_INDEX, _DLEN-1);
            }

            packetArray[packetArray.Length-1] = _CHK;

            return packetArray;
        }
    }
}
