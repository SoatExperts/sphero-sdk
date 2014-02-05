using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Communication
{
    /// <summary>
    /// Exception when the bluetooth is deactived 
    /// </summary>
    public class BluetoothDeactivatedException : Exception
    {
        public BluetoothDeactivatedException(Exception ex)
                : base("The bluetooth is deactivated", ex)
            {

            }
    }
}
