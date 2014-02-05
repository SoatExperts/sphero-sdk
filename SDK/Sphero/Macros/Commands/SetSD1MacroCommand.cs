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
    public class SetSD1MacroCommand : MacroCommand
    {
        public int SD1 { get; set; }
        public SetSD1MacroCommand()
        {
            CommandID = (byte)MacroCommandID.SetSD1;
        }

        public override byte[] ToArray()
        {
            byte[] intBytes = ByteHelper.IntegerToBytes(SD1);
            return new byte[] { CommandID, intBytes[0], intBytes[1] };
        }
    }
}
