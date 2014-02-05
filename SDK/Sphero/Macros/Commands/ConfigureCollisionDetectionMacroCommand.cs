using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros.Commands
{
    /// <summary>
    /// This configures the collision detection subsystem that ties in with the Branch On Collision command. 
    /// Rather than reproduce the details here, please refer to the collision detection document.
    /// Note that there is no PCD for this command. 
    /// </summary>
    public  class ConfigureCollisionDetectionMacroCommand : MacroCommand
    {

        public ConfigureCollisionDetectionMacroCommand()
        {
            CommandID = (byte)MacroCommandID.ConfigureCollisionDetection;
        }

        public override byte[] ToArray()
        {
            // TODO : Implement this macroCOmmand
            throw new NotImplementedException();
        }
    }
}
