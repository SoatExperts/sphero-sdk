using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This emits an asynchronous message to the client with the supplied marker value. 
    /// Marker value zero is reserved for the end of macro option, so don't emit it unless you want to confuse yourself. 
    /// The format of the async message payload is below
    /// Marker  MacroID Command#    Command#
    /// 
    /// Async ID code 06h is reserved for macro notifications, the Marker field comes from the command and the last two bytes are the command number of this marker within the current macro. 
    /// You can read more about async messages in the Sphero API document. 
    /// </summary>
    public class EmitMarkerMacroCommand : MacroCommand
    {

        public EmitMarkerMacroCommand()
        {
            CommandID = (byte)MacroCommandID.EmitMarker;
        }
        public override byte[] ToArray()
        {
            // TODO : implement this method
            throw new NotImplementedException();
        }
    }
}
