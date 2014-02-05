using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Locator
{
    public class ThreeAxis
    {
        private short _x;
        private short _y;
        private short _z;

        public short X { get { return _x; } }
        public short Y { get { return _y; } }
        public short Z { get { return _z; } }

        internal ThreeAxis() { }
        
        internal ThreeAxis(short x, short y, short z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }
}
