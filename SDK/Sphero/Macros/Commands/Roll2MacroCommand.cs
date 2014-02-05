using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This is just like the Roll command but it accepts a 2-byte delay value.
    /// </summary>
    public class Roll2MacroCommand : MacroCommand
    {
         public byte Speed { get; set; }

        public short Heading { get; set; }

        public int Delay { get; set; }

        public Roll2MacroCommand()
        {
            CommandID = (byte)MacroCommandID.Roll2;
        }


        public override byte[] ToArray()
        {
            byte[] headingBuffer = ByteHelper.IntegerToBytes(Heading);
            byte[] delayBuffer = ByteHelper.IntegerToBytes(Delay);

            return new byte[] { CommandID, Speed, headingBuffer[0], headingBuffer[1], headingBuffer[0], headingBuffer[1] };
        }
    }
}
