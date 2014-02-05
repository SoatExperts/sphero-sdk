using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This controls the intensity of the blue "aiming" LED. That's it
    /// </summary>
    public class SetBackLEDMacroCommand : MacroCommand
    {
        public byte Intensity { get; set; }

        public byte PCD { get; set; }

        public SetBackLEDMacroCommand()
        {
            CommandID = (byte)MacroCommandID.SetBackLED;
        }


        public override byte[] ToArray()
        {
            return new byte[] { CommandID, Intensity, PCD };
        }
    }
}
