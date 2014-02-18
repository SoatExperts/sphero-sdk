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
using Sphero.Collisions;
using System.Diagnostics;

namespace CollisionSample
{
    public partial class DrivePage : PhoneApplicationPage
    {
        private SpheroDevice _spheroDevice;
        public DrivePage()
        {
            InitializeComponent();

            if (App.CurrentConnection != null)
            {
                MessageBox.Show(string.Format("Connected to {0}", App.CurrentConnection.BluetoothName));
                _spheroDevice = new SpheroDevice(App.CurrentConnection);
                _spheroDevice.SetBackLED(1.0f);

                // Configure collision detection and subscribe to detection event
                _spheroDevice.ConfigureCollisionDetection(CollisionMethod.Enable, 125, 125, 125, 125, 50);
                _spheroDevice.CollisionDetected += _spheroDevice_CollisionDetected;

                spheroJoystick.Start();
            }
            else
                NavigationService.GoBack();
        }

        void _spheroDevice_CollisionDetected(CollisionData data)
        {
            // Collision detected !
            Debug.WriteLine("Collision ! ");
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if(_spheroDevice != null)
                _spheroDevice.SetBackLED(0.0f);

            spheroJoystick.Stop();
            if (App.CurrentConnection != null)
            {
                await App.CurrentConnection.Disconnect();
                App.CurrentConnection = null;
            }
            base.OnNavigatingFrom(e);
        }

        private void spheroJoystick_Calibrating(object sender, Sphero.Controls.JoystickCalibrationEventArgs e)
        {
            if (_spheroDevice != null)
            {
                _spheroDevice.Roll(e.Angle, 0);
            }
        }

        private void spheroJoystick_CalibrationReleased(object sender, Sphero.Controls.JoystickCalibrationEventArgs e)
        {
            if (_spheroDevice != null)
            {
                _spheroDevice.SetHeading(0);
            }
        }

        private void spheroJoystick_Moving(object sender, Sphero.Controls.JoystickMoveEventArgs e)
        {
            if (_spheroDevice != null)
            {
                _spheroDevice.Roll(e.Angle, e.Speed);
            }
        }

        private void spheroJoystick_Released(object sender, Sphero.Controls.JoystickMoveEventArgs e)
        {
            if (_spheroDevice != null)
            {
                _spheroDevice.Roll(e.Angle, 0);
            }
        }
    }
}