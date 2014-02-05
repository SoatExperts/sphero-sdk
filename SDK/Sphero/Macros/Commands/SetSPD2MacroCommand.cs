using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// One of  system speed settings are provided. Certain roll commands use these values in place of explicit speed values. 
    /// </summary>
    public class SetSPD2MacroCommand : MacroCommand
    {
        public int SPD2 { get; set; }
        public SetSPD2MacroCommand()
        {
            CommandID = (byte)MacroCommandID.SetSPD2;
        }

        public override byte[] ToArray()
        {
            byte[] intBytes = ByteHelper.IntegerToBytes(SPD2);
            return new byte[] { CommandID, intBytes[0], intBytes[1] };
        }
    }
}
