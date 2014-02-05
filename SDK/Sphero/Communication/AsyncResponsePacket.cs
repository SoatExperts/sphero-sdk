using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Communication
{

    internal class AsyncResponsePacket
    {
        private const byte DATA_START_INDEX = 5;

        private byte _SOP1;
        private byte _SOP2;
        private byte _IDCODE;
        private byte _DLENMSB;
        private byte _DLENLSB;
        private byte[] _data;
        private byte _CHK;

        public AsynchronousId ResponseCode { get { return (AsynchronousId)_IDCODE; } }
        public byte[] Data { get { return _data; } }

        public AsyncResponsePacket(byte[] responseBuffer)
        {
            if (responseBuffer == null)
                return;

            if (responseBuffer.Length < 6)
                return;

            _SOP1 = responseBuffer[0];
            _SOP2 = responseBuffer[1];
            _IDCODE = responseBuffer[2];
            _DLENMSB = responseBuffer[3];
            _DLENLSB = responseBuffer[4];

            int length = BitConverter.ToUInt16(new byte[] { _DLENLSB, _DLENMSB }, 0);

            if (length > 1)
            {
                _data = new byte[length - 1];
                Array.Copy(responseBuffer, 5, _data, 0, length - 1);
            }

            _CHK = responseBuffer[responseBuffer.Length - 1];
        }

        /// <summary>
        /// Format the packet to array
        /// </summary>
        /// <returns>Formatted packet</returns>
        public byte[] ToArray()
        {
            int length = BitConverter.ToUInt16(new byte[] { _DLENLSB, _DLENMSB }, 0);
            byte[] packetArray = new byte[length + 5];
            packetArray[0] = _SOP1;
            packetArray[1] = _SOP2;
            packetArray[2] = _IDCODE;
            packetArray[3] = _DLENMSB;
            packetArray[4] = _DLENLSB;

            if (_data != null)
            {
                Array.Copy(_data, 0, packetArray, DATA_START_INDEX, length - 1);
            }

            packetArray[packetArray.Length - 1] = _CHK;

            return packetArray;
        }
    }
}
