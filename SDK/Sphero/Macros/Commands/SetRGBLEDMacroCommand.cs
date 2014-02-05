using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This command drives the RGB LED to the desired values. When macros are running, RGB LED commands take precedence over all others in the system (except for battery warnings).
    /// </summary>
    public class SetRGBLEDMacroCommand : MacroCommand
    {
        public byte Red { get; set; }
        public byte Green { get; set; }

        public byte Blue { get; set; }

        public byte PCD { get; set; }

        public SetRGBLEDMacroCommand()
        {
            CommandID = (byte)MacroCommandID.SetRGBLED;
        }

        public override byte[] ToArray()
        {
            return new byte[] { CommandID, Red, Green, Blue, PCD };
        }
    }
}
