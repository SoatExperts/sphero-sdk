using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Locator
{
    public class Quaternion
    {
        private short _q0;
        private short _q1;
        private short _q2;
        private short _q3;

        public short Q0 { get { return _q0; } }
        public short Q1 { get { return _q1; } }
        public short Q2 { get { return _q2; } }
        public short Q3 { get { return _q3; } }

        internal Quaternion() { }

        internal Quaternion(short q0, short q1, short q2, short q3)
        {
            _q0 = q0;
            _q1 = q1;
            _q2 = q2;
            _q3 = q3;
        }
    }
}
