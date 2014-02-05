using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This is just like the roll command  but the PCD is omitted and instead derived from the SD1 value.
    /// </summary>
    public class RollWithSD1MacroCommand : MacroCommand
    {
        public int Heading { get; set; }

        public byte Speed{ get; set; }

        public RollWithSD1MacroCommand()
        {
            CommandID = (byte)MacroCommandID.RollWithSD1;
        }

        public override byte[] ToArray()
        {
            byte[] headingBuffer = ByteHelper.IntegerToBytes(Heading);

            return new byte[] { CommandID, headingBuffer[0], headingBuffer[1], Speed };
        }
    }
}
