using Sphero.Macros.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros
{
    /// <summary>
    /// Contains Macro data
    /// </summary>
    public class Macro
    {
        private MacroType _type;
        private byte _id;
        private byte _flags;
        //private byte _extFlags;


        public byte ID { get { return _id; } }

        /// <summary>
        /// Contains all commands the macro will execute
        /// </summary>
        public List<MacroCommand> Commands { get; set; }

        /// <summary>
        /// Get or Set the different behaviors of the Macro
        /// </summary>
        public byte Flags
        {
            get { return _flags; }
            set { _flags = value; }
        }


        /// <summary>
        /// Create a new macro
        /// </summary>
        /// <param name="type">Type of the macro : Permanent, Temporary, Stream</param>
        /// <param name="id">ID of the macro</param>
        public Macro(MacroType type, byte id)
        {
            if (type != MacroType.Permanent)
                _id = (byte)type;
            else
                _id = id;

            _type = type;
            _flags = 0x00;
            //_extFlags = 0x00;

            Commands = new List<MacroCommand>();
        }

        /// <summary>
        /// Create the array of bytes representation of the Macro
        /// </summary>
        /// <returns>Array of bytes containing macro data</returns>
        public byte[] ToArray()
        {
            byte[] commandBuffer = { };

            if (Commands.OfType<EndMacroCommand>() == null || !Commands.OfType<EndMacroCommand>().Any())
            {
                Commands.Add(new EndMacroCommand());
            }

            foreach (MacroCommand command in Commands)
            {
                // Get the array version of the command
                byte[] commandArray = command.ToArray();

                // Create a temporary buffer to concat the command with the actual commandbuffer
                byte[] tempBuffer = new byte[commandBuffer.Length + commandArray.Length];
                Array.Copy(commandBuffer, tempBuffer, commandBuffer.Length);
                Array.Copy(commandArray, 0, tempBuffer, commandBuffer.Length, commandArray.Length);
                
                // Set the command buffer with the concatened command
                commandBuffer = tempBuffer;
            }

            // Checking if the last byte is the "Macro End" one
            //if(commandBuffer[commandBuffer.Length-1] != (byte)MacroCommandID.MacroEnd)
            //{
            //    // Create a temporary buffer to concat the "Macro End" byte with the actual commandbuffer
            //    byte[] tempBuffer = new byte[commandBuffer.Length + 1];
            //    Array.Copy(commandBuffer, tempBuffer, commandBuffer.Length);
            //    tempBuffer[tempBuffer.Length - 1] = (byte)MacroCommandID.MacroEnd;

            //    // Set the command buffer with the concatened "Macro End" byte
            //    commandBuffer = tempBuffer;
            //}

            
            byte[] finalBuffer = new byte[commandBuffer.Length + 2];
            
            // Setting header of the finalBuffer
            finalBuffer[0] = _id;
            finalBuffer[1] = _flags;
            //finalBuffer[2] = _extFlags;

            // Adding the Command part
            Array.Copy(commandBuffer, 0, finalBuffer, 2, commandBuffer.Length);

            return finalBuffer;
        }
    }
}
