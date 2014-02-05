using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros
{
    /// <summary>
    /// Contains differents flags representing some behaviors
    /// </summary>
    public static class MacroFlags
    {
        /// <summary>
        /// Kills the drive motors automatically on exit or abort. 
        /// If the stabilization system is enabled, this executes a "stop roll" command, 
        /// otherwise zero PWMs are sent to the motor drivers.
        /// </summary>
        public const byte MF_MOTOR_CONTROL = 0x01;

        /// <summary>
        /// Gives the macro exclusive control of driving, excluding commands from the Bluetooth client and orbBasic.
        /// </summary>
        public const byte MF_EXCLUSIVE_DRV = 0x02;

        /// <summary>
        /// Allow Stop on Disconnected behavior if a macro is running; normally macros are immune to this
        /// </summary>
        public const byte MF_ALLOW_SOD = 0x04;

        /// <summary>
        /// Inhibit execution of this macro if a smartphone client is connected.
        /// </summary>
        public const byte MF_INH_IF_CONN = 0x08;

        /// <summary>
        /// Emit a macro marker with parameter 00h when the end of the macro is reached.
        /// </summary>
        public const byte MF_ENDSIG = 0x10;

        /// <summary>
        /// Macro execution does NOT reset the client inactivity (sleep) timer.
        /// </summary>
        public const byte MF_STEALTH = 0x20;

        /// <summary>
        /// This macro cannot be aborted (killed). This is only valid for system macros and ignored for all other macro ID types.
        /// </summary>
        public const byte MF_UNKILLABLE = 0x40;

        /// <summary>
        /// The extended flags byte is present and follows this one. (I ran out of bits in this flag, so there is an extended one.)
        /// </summary>
        public const byte MF_EXT_FLAGS = 0x80;
    }
}
