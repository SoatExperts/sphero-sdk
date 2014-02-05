using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Internal
{
    /// <summary>
    /// Provide helper methods about bytes manipulation
    /// </summary>
    internal static class ByteHelper
    {
        /// <summary>
        /// Get a byte array corresponding to an integer value
        /// </summary>
        /// <param name="integer">Integer value to "convert"</param>
        /// <returns>Byte array</returns>
        public static byte[] IntegerToBytes(int integer)
        {
            Byte num = (Byte)((integer & 65280) >> 8);
            return new Byte[] { num, (Byte)(integer & 255) };
        }

        /// <summary>
        /// Convert an array of ascii bytes into string
        /// </summary>
        /// <param name="bytes">Array to convert</param>
        /// <returns>String converted</returns>
        internal static string AsciiToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length);
            foreach (byte b in bytes)
            {
                if (b > 0)
                    sb.Append(b <= 0x7f ? (char)b : '?');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get short value corresponding to a byte array
        /// </summary>
        /// <param name="bytes">Array bytes to "convert"</param>
        /// <returns>Short value corresponding</returns>
        public static short BytesToShort(byte[] bytes)
        {
            if (bytes == null)
                return 0;

            if (bytes.Length != 2)
                return 0;

            short value = bytes[0];
            value = (short)(value << 8);
            value += bytes[1];
            return value;
        }

        /// <summary>
        /// Get short value corresponding to a byte array
        /// </summary>
        /// <param name="bytes">Array bytes to "convert"</param>
        /// <returns>Integer value corresponding</returns>
        public static int BytesToInteger(byte[] bytes)
        {
            if (bytes == null)
                return 0;

            if (bytes.Length != 4)
                return 0;

            int value = bytes[3];
            value += bytes[2] << 8;
            value += bytes[1] << 8 << 8;
            value += bytes[0] << 8 << 8 << 8;
            return value;
        }
    }


  
}
