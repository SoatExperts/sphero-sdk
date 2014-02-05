using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This is the ultimate in roll commands: the speed comes from one of the system speed values and the post command delay from SD1. All you need to provide is a heading.
    /// </summary>
    public class RollAtSPD1WithSD1MacroCommand : MacroCommand
    {
        public int Heading { get; set; }

        public RollAtSPD1WithSD1MacroCommand()
        {
            CommandID = (byte)MacroCommandID.RollAtSPD1WithSD1;
        }

        public override byte[] ToArray()
        {
            byte[] headingBuffer = ByteHelper.IntegerToBytes(Heading);
            return new byte[] { CommandID, headingBuffer[0], headingBuffer[1] };
        }
    }
}
