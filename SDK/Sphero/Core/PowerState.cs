using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Core
{
    /// <summary>
    /// Represent the possible sphero power states
    /// </summary>
    public enum PowerState
    {
        /// <summary>
        /// Battery is charging
        /// </summary>
        CHARGING = 0x01,

        /// <summary>
        /// Battery life is ok
        /// </summary>
        OK = 0x02,

        /// <summary>
        /// Battery life is low
        /// </summary>
        LOW = 0x03,

        /// <summary>
        /// Battery life is critical
        /// </summary>
        CRITICAL = 0x04
    }
}
