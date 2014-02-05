using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Communication
{
    /// <summary>
    /// Command packet to sent to a Sphero device
    /// </summary>
    public class CommandPacket
    {
        #region Constants 

        // Start of packet 1
        private const byte SOP1 = 0xFF;

        // Minimum length of data packet (containing checksum)
        private const int MIN_DLEN = 1;

        // Index where data starts
        private const int DATA_START_INDEX = 6;

        #endregion

        #region Fields

        // Start of packet 1
        private byte _SOP2;

        // Device ID
        private byte _DID;

        // Command ID
        private byte _CID;

        // Sequence number
        private byte _SEQ;

        // Data
        private byte[] _data;

        #endregion

        /// <summary>
        /// Create a command packet
        /// </summary>
        /// <param name="startOfPacket2">F8 to FFh encoding 4 bits of per-message options</param>
        /// <param name="deviceID">The virtual device this packet is intended for</param>
        /// <param name="commandID">The command code</param>
        /// <param name="data">Optional data to accompany the Command </param>
        public CommandPacket(byte startOfPacket2, byte deviceID, byte commandID, byte[] data)
        {
            this._SOP2 = startOfPacket2;
            this._DID = deviceID;
            this._CID = commandID;
            this._SEQ = 0x01;
            this._data = data;
        }

        /// <summary>
        /// True if the command request need a response packet from sphero
        /// </summary>
        public bool NeedResponse { get { return _SOP2 == 0xFF; } }

        /// <summary>
        /// Format the packet to send it
        /// </summary>
        /// <returns>Formatted packet</returns>
        public byte[] ToArray()
        {
            int dataLength = 0;

            // Set data length
            if (this._data != null)
                dataLength = _data.GetLength(0);

            // Initialize the array with the good length
            byte[] packetArray = new byte[dataLength + 7];

            // Set header of the packet
            packetArray[0] = SOP1;
            packetArray[1] = _SOP2;
            packetArray[2] = _DID;
            packetArray[3] = _CID;
            packetArray[4] = _SEQ;
            packetArray[5] = (byte)(dataLength + 1);

            // If there is data, copy them into the packet array
            if(_data != null)
            {
                Array.Copy(_data, 0, packetArray, DATA_START_INDEX, dataLength);
            }

            // Insert checksum
            packetArray[dataLength + 6] = ComputeCheckSum(packetArray);

            // return the array representation of the packet
            return packetArray;
        }

        /// <summary>
        /// Set the current sequence number of the packet
        /// </summary>
        /// <param name="seqNum">Sequence number of the packet to set</param>
        public void SetSequenceNumber(byte seqNum)
        {
            this._SEQ = seqNum;
        }

        /// <summary>
        /// Compute the checksum byte of the packet
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private byte ComputeCheckSum(byte[] command)
        {
            // Initialize checksum value
            byte checksum = 0;

            // Sum all array values into checksum
            for (int i = 2; i < command.GetLength(0) - 1; i++)
                checksum += command[i];

            // Return bitwise complement
            return (byte)~checksum;

        }
    }
}
