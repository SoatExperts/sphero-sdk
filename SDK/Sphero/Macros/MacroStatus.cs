using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Macros
{
    public class MacroStatus
    {
        private int _id;
        private int _commandNumber;

        public int ID { get { return _id; } }

        public int CommandNumber { get { return _commandNumber; } }

        public MacroStatus(byte[] data)
        {
            _id = data[0];
            _commandNumber = data[1] << 8;
            _commandNumber += data[2];
        }
    }
}
