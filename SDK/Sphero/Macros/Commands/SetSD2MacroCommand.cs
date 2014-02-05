using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// One of system delay settings are provided. Certain commands inherit these values in place of the PCD byte. 
    /// </summary>
    public class SetSD2MacroCommand : MacroCommand
    {
        public int SD2 { get; set; }
        public SetSD2MacroCommand()
        {
            CommandID = (byte)MacroCommandID.SetSD2;
        }

        public override byte[] ToArray()
        {
            byte[] intBytes = ByteHelper.IntegerToBytes(SD2);
            return new byte[] { CommandID, intBytes[0], intBytes[1] };
        }
    }
}
