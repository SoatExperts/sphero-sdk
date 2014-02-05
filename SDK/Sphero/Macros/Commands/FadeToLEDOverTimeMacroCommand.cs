using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This powerful command fades the RGB LED from its current value to the provided one over the time provided. 
    /// The current LED value is from the last LED macro command. 
    /// Intermediate colors are derived from the individual fractional movements of each of the red, green and blue components, not some clever movement through the color space. 
    /// 
    /// NOTE: This command runs in the background so you will need to provide a suitable delay to allow it to complete. 
    /// </summary>
    public class FadeToLEDOverTimeMacroCommand : MacroCommand
    {
        public byte Red { get; set; }
        public byte Green { get; set; }

        public byte Blue { get; set; }

        public int Time { get; set; }

        public FadeToLEDOverTimeMacroCommand()
        {
            CommandID = (byte)MacroCommandID.FadeToRGB;
        }
        public override byte[] ToArray()
        {
            byte[] timeBuffer = ByteHelper.IntegerToBytes(Time);
            return new byte[] { CommandID, Red, Green, Blue, timeBuffer[0], timeBuffer[1] };
        }
    }
}
