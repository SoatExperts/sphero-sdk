using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Core
{
    /// <summary>
    /// List of device identifiers
    /// </summary>
    public static class DeviceIDs
    {
        /// <summary>
        /// The core - 0x00
        /// </summary>
        public const byte CORE = 0x00;

        /// <summary>
        /// Bootloader - 0x01
        /// </summary>
        public const byte BOOTLOADER = 0x01;

        /// <summary>
        /// SPHERO - 0x02
        /// </summary>
        public const byte SPHERO = 0x02;

    }
}
