using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Locator
{
    public class TwoAxis
    {
        private short _x;
        private short _y;

        public short X { get { return _x; } }
        public short Y { get { return _y; } }


        internal TwoAxis() { }

        internal TwoAxis(short x, short y)
        {
            _x = x;
            _y = y;
        }
    }
}
