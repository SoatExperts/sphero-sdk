using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;

namespace Sphero.Communication
{
    /// <summary>
    /// Sphero information like DisplayName, HostName
    /// </summary>
    public class SpheroInformation
    {
        /// <summary>
        /// Bluetooth displayed name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Bluetooth adress informations
        /// </summary>
        public HostName HostName { get; set; }

        /// <summary>
        /// Create SpheroInformation instance
        /// </summary>
        /// <param name="displayName">DisplayName of the device</param>
        /// <param name="hostName">HostName information of the device</param>
        public SpheroInformation(string displayName, HostName hostName)
        {
            this.DisplayName = displayName;
            this.HostName = hostName;
        }

        
    }
}
