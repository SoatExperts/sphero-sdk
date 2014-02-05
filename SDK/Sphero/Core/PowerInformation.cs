using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Core
{
    /// <summary>
    /// Contains power and battery information
    /// </summary>
    public class PowerInformation
    {
        #region Fields

        private short _recVersion;
        private PowerState _powerState;
        private ushort _batteryVoltage;
        private ushort _numberOfRecharges;
        private ushort _timeSinceLastRecharge;

        #endregion

        #region Constructor

        internal PowerInformation(byte[] data)
        {
            if (data == null)
                return;

            if (data.Length != 8)
                return;

            _recVersion = data[0];
            _powerState = (PowerState)data[1];
            _batteryVoltage = BitConverter.ToUInt16(new byte[] { data[3], data[2] }, 0);
            _numberOfRecharges = BitConverter.ToUInt16(new byte[] { data[5], data[4] }, 0);
            _timeSinceLastRecharge = BitConverter.ToUInt16(new byte[] { data[7], data[6] }, 0);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Record version code
        /// </summary>
        public short RecordVersion { get { return this._recVersion; } }

        /// <summary>
        /// High-level state of the power syste
        /// </summary>
        public PowerState PowerState { get { return this._powerState; } }

        /// <summary>
        /// Current battery voltage scaled in 100ths of a volt; 02EFh would be 7.51 volts
        /// </summary>
        public ushort BatteryVoltage { get { return this._batteryVoltage; } }

        /// <summary>
        /// Number of battery recharges in the life of this Sphero 
        /// </summary>
        public ushort TotalRecharges { get { return this._numberOfRecharges; } }

        /// <summary>
        /// Seconds awake since last recharge
        /// </summary>
        public ushort SecondsSinceLastRecharge { get { return this._timeSinceLastRecharge; } }

        #endregion
    }
}
