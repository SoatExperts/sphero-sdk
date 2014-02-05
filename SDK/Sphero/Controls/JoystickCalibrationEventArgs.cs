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
    public class JoystickCalibrationEventArgs : RoutedEventArgs
    {
        public JoystickCalibrationEventArgs()
        {

        }

        public int Angle { get; set; }

    }
}
