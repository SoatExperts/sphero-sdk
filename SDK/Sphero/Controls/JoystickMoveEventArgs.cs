using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETFX_CORE
using Windows.UI.Xaml;
#else
using System.Windows;
#endif


namespace Sphero.Controls
{
    public class JoystickMoveEventArgs : RoutedEventArgs
    {
        public JoystickMoveEventArgs()
        {

        }

        public int Angle { get; set; }

        public float Speed { get; set; }
    }
}
