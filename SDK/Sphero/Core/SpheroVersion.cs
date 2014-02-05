using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Core
{
    /// <summary>
    /// Contains Sphero versioning informations
    /// </summary>
    public class SpheroVersion
    {
        #region Fields

        private int _recordVersion;
        private int _modelNumber;
        private int _hardwareVersion;
        private int _msaVersion;
        private int _msaRevision;
        private float _blVersion;
        private float _orbBasicVersion;
        private float _meVersion;

        #endregion

        #region Constructor

        internal SpheroVersion(byte[] data)
        {
            if (data == null)
                return;

            if( data.Length < 8)
                return;

            _recordVersion = data[0];
            _modelNumber = data[1];
            _hardwareVersion = data[2];
            _msaVersion = data[3];
            _msaRevision = data[4];
            _blVersion = (float)data[5] / 10.0f;
            _blVersion = (float)data[5] / 10.0f;
            _meVersion = (float)data[7] / 10.0f;
            _orbBasicVersion = 0;


        }

        #endregion

        #region Properties

        /// <summary>
        /// This record version number. This will increase when more resources are added. 
        /// </summary>
        public int RecordVersion { get { return this._recordVersion; } }

        /// <summary>
        /// Model number
        /// </summary>
        public int ModelNumber { get { return this._modelNumber; } }

        /// <summary>
        /// Hardware version code (ranges 1 through 9) 
        /// </summary>
        public int HardwareVersion { get { return this._hardwareVersion; } }

        /// <summary>
        /// Main Sphero Application version
        /// </summary>
        public int MainSpheroApplicationVersion { get { return this._msaVersion; } }

        /// <summary>
        /// Sphero Application revision
        /// </summary>
        public int MainSpheroApplicationRevision { get { return this._msaRevision; } }

        /// <summary>
        /// Bootloader version
        /// </summary>
        public float BootLoaderVersion { get { return this._blVersion; } }

        /// <summary>
        /// orbBasic version
        /// </summary>
        public float OrbBasicVersion { get { return this._orbBasicVersion; } }

        /// <summary>
        /// Macro executive version
        /// </summary>
        public float MacroExecutiveVersion { get { return this._meVersion; } }


        #endregion
    }
}
