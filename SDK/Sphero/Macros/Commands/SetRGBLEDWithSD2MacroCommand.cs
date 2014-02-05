using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// Just like the command but the delay is inherited from SD2.   
    /// </summary>
    public class SetRGBLEDWithSD2MacroCommand : MacroCommand
    {
        public byte Red { get; set; }
        public byte Green { get; set; }

        public byte Blue { get; set; }

        public SetRGBLEDWithSD2MacroCommand()
        {
            CommandID = (byte)MacroCommandID.SetRGBLED;
        }

        public override byte[] ToArray()
        {
            return new byte[] { CommandID, Red, Green, Blue };
        }
    }
}
