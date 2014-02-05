using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Core
{
    /// <summary>
    /// Contains information that identifies sphero over bluetooth
    /// </summary>
    public class BluetoothInformation
    {
        #region Fields

        private byte[] _asciiName;
        private byte[] _btAdress;
        private byte _empty;
        private byte[] _idColors;

        #endregion

        #region Constructor

        internal BluetoothInformation(byte [] responseData)
        {
            if (responseData == null)
                return;

            if (responseData.Length != 32)
                return;

            _asciiName = new byte[16];
            Array.Copy(responseData, _asciiName, 16);

            _btAdress = new byte[12];
            Array.Copy(responseData, 16, _btAdress, 0, 12);

            _empty = responseData[28];

            _idColors = new byte[3];
            Array.Copy(responseData, 29, _idColors, 0, 3);

        }

        #endregion

        #region Properties

        /// <summary>
        /// The display name of the sphero device over bluetooth
        /// </summary>
        public string Name { get { return ByteHelper.AsciiToString(_asciiName); } }

        /// <summary>
        /// The bluetooth adress of the sphero device
        /// </summary>
        public string BluetoothAdress { get { return Encoding.UTF8.GetString(_btAdress, 0, _btAdress.Length); } }

        #endregion
    }
}
