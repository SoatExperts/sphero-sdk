using Sphero.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This puts Sphero to sleep, able to be awaken from a double-shake. 
    /// The time parameter is optional and is the number of milliseconds for him to automatically reawaken.
    /// If set to zero, he goes to sleep forever.
    /// If set to FFFFh the actual time is inherited from the API command (DID 00h, CID 22h
    /// Which just proves that the API command calls a system macro which implements this. 
    /// </summary>
    public class GoToSleepNowMacroCommand : MacroCommand
    {
        public int Time { get; set; }

        public GoToSleepNowMacroCommand()
        {
            CommandID = (byte)MacroCommandID.GoToSleep;
        }

        public override byte[] ToArray()
        {
            byte[] timeBuffer = ByteHelper.IntegerToBytes(Time);
            return new byte[] { CommandID, timeBuffer[0], timeBuffer[1] };
        }
    }
}
