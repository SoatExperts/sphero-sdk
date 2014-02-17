using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sphero.Devices;

namespace IMUSample
{
    public partial class CalibrationPage : PhoneApplicationPage
    {
        private SpheroDevice _spheroDevice;
        public CalibrationPage()
        {
            InitializeComponent();

            if (App.CurrentConnection != null)
            {
                _spheroDevice = new SpheroDevice(App.CurrentConnection);

                // OFF Stabilization et set LEDs
                _spheroDevice.StabilizationOFF();
                _spheroDevice.SetRGBLED(0.5f, 0, 0);
                _spheroDevice.SetBackLED(1.0f);
            }
            else
                NavigationService.GoBack();
        }

        private void btnCalibrationDone_Click(object sender, RoutedEventArgs e)
        {
            // Initialize heading and go to the reading page
            _spheroDevice.SetHeading(0);
            NavigationService.Navigate(new Uri("/IMUReadingPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}