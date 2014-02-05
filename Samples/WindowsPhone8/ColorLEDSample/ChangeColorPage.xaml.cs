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

namespace ColorLEDSample
{
    public partial class ChangeColorPage : PhoneApplicationPage
    {
        private SpheroDevice _spheroDevice;
        public ChangeColorPage()
        {
            InitializeComponent();

            if (App.CurrentConnection != null)
            {
                MessageBox.Show(string.Format("Connected to {0}", App.CurrentConnection.BluetoothName));
                _spheroDevice = new SpheroDevice(App.CurrentConnection);
            }
            else
                NavigationService.GoBack();
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (App.CurrentConnection != null)
            {
                await App.CurrentConnection.Disconnect();
                App.CurrentConnection = null;
            }
            base.OnNavigatingFrom(e);
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            if(_spheroDevice != null)
            {
                _spheroDevice.SetRGBLED(1.0f, 0, 0);
            }
        }

        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            if (_spheroDevice != null)
            {
                _spheroDevice.SetRGBLED( 0, 1.0f, 0);
            }
        }

        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            if (_spheroDevice != null)
            {
                _spheroDevice.SetRGBLED(0,0,1.0f);
            }
        }
    }
}