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
using Sphero.Locator;

namespace IMUSample
{
    public partial class IMUReadingPage : PhoneApplicationPage
    {
        private SpheroDevice _spheroDevice;
        public IMUReadingPage()
        {
            InitializeComponent();

            if (App.CurrentConnection != null)
            {
                MessageBox.Show(string.Format("Connected to {0}", App.CurrentConnection.BluetoothName));
                _spheroDevice = new SpheroDevice(App.CurrentConnection);

                // Initialize SensorData streaming Masks
                _spheroDevice.SetDataStreaming(20, 1, Masks.FilteredIMU, 1);

                // Subscribe to SensorData event
                _spheroDevice.SensorDataNotification += _spheroDevice_SensorDataNotification;
            }
            else
                NavigationService.GoBack();
        }

        void _spheroDevice_SensorDataNotification(Sphero.Locator.SensorData data)
        {
            // If IMU data are present
            if (data.FilteredIMU != null)
            {
                // Update Sliders with data on UI Thread
                Dispatcher.BeginInvoke(() =>
                {
                    sldPitch.Value = data.FilteredIMU.Pitch;
                    sldRoll.Value = data.FilteredIMU.Roll;
                    sldYaw.Value = data.FilteredIMU.Yaw;
                });
               
            }
        }
    }
}