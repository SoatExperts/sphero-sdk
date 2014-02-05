using Sphero.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This allows you to take over one or both of the motor output values, instead of having the stabilization system control them. 
    /// Each motor (left and right) requires a mode (see below) and a power value from 0- FFh. 
    /// This command will disable stabilization if both modes aren't "ignore" so you'll need to re-enable it once you're done.
    /// </summary>
    public class SendRawMotorMacroCommand : MacroCommand
    {
        public MotorMode LeftMode { get; set; }
        public byte LeftPower { get; set; }

        public MotorMode RightMode { get; set; }
        public byte RightPower { get; set; }

        public byte PCD { get; set; }

        public SendRawMotorMacroCommand()
        {
            CommandID = (byte)MacroCommandID.SendRawMotorValues;
        }

        public override byte[] ToArray()
        {
            return new byte[] { CommandID, (byte)LeftMode, LeftPower, (byte)RightMode, RightPower, PCD };
        }
    }
}
