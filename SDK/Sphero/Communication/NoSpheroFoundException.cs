using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphero.Communication
{
    /// <summary>
    /// Exception when no sphero is found when discovering
    /// </summary>
    public class NoSpheroFoundException : Exception
    {
        public NoSpheroFoundException(Exception ex)
            : base("No Sphero Found", ex)
        {

        }
    }
}
