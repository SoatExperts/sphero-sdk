using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sphero.Macros;
using Sphero.Macros.Commands;
using Sphero.Devices;
using Sphero.Core;
using Sphero.Communication;

namespace MacroSample
{
    public partial class MacroPage : PhoneApplicationPage
    {
        private SpheroDevice _spheroDevice;

        public MacroPage()
        {
            InitializeComponent();

            if (App.CurrentConnection != null)
            {
                MessageBox.Show(string.Format("Connected to {0}", App.CurrentConnection.BluetoothName));
                
                _spheroDevice = new SpheroDevice(App.CurrentConnection);

                _spheroDevice.ReinitMacroExecutive(response =>
                    {
                        if (response == MessageResponseCode.ORBOTIX_RSP_CODE_OK)
                        {
                            Macro flipMacro = new Macro(MacroType.Permanent, 102);
                            flipMacro.Commands.Add(new SendRawMotorMacroCommand
                            {
                                LeftMode = MotorMode.Forward,
                                LeftPower = 255,
                                RightMode = MotorMode.Forward,
                                RightPower = 255,
                                PCD = 255
                            });
                            flipMacro.Commands.Add(new DelayMacroCommand { Time = 150 });
                            flipMacro.Commands.Add(new SendRawMotorMacroCommand
                            {
                                LeftMode = MotorMode.Off,
                                LeftPower = 0,
                                RightMode = MotorMode.Off,
                                RightPower = 0,
                                PCD = 255
                            });
                            flipMacro.Commands.Add(new SetStabilizationMacroCommand
                            {
                                Flag = StabilizationStatus.OnWithoutReset,
                                PCD = 255
                            });
                            _spheroDevice.SaveMacro(flipMacro, null);
                        }
                    });
               
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

        private void btnRainbow_Click(object sender, RoutedEventArgs e)
        {
            Macro rainbowMacro = new Macro(MacroType.Temporary, 255);
           
            rainbowMacro.Commands.Add(new LoopStartMacroCommand() { Count = 5 });
            rainbowMacro.Commands.Add(new FadeToLEDOverTimeMacroCommand()
            {
                Red = 0xFF,
                Green = 0,
                Blue = 0,
                Time = 200
            });
            rainbowMacro.Commands.Add(new DelayMacroCommand { Time = 200 });
            rainbowMacro.Commands.Add(new FadeToLEDOverTimeMacroCommand()
            {
                Red = 0xFF,
                Green = 0x7E,
                Blue = 0x6D,
                Time = 200
            });
            rainbowMacro.Commands.Add(new DelayMacroCommand { Time = 200 });
            rainbowMacro.Commands.Add(new FadeToLEDOverTimeMacroCommand()
            {
                Red = 0x7F,
                Green = 0xFF,
                Blue = 0x00,
                Time = 200
            });
            rainbowMacro.Commands.Add(new DelayMacroCommand { Time = 200 });
            rainbowMacro.Commands.Add(new FadeToLEDOverTimeMacroCommand()
            {
                Red = 0x00,
                Green = 0xFF,
                Blue = 0x00,
                Time = 200
            });
            rainbowMacro.Commands.Add(new DelayMacroCommand { Time = 200 });
            rainbowMacro.Commands.Add(new FadeToLEDOverTimeMacroCommand()
            {
                Red = 0xDE,
                Green = 0x00,
                Blue = 0xFF,
                Time = 200
            });
            rainbowMacro.Commands.Add(new DelayMacroCommand { Time = 200 });
            rainbowMacro.Commands.Add(new LoopEndMacroCommand());

            
            _spheroDevice.SaveTemporaryMacro(rainbowMacro, response =>
            {
                if(response == MessageResponseCode.ORBOTIX_RSP_CODE_OK)
                {
                    _spheroDevice.RunTemporaryMacro(null);
                }
            });
        }

        private void btnFlipClick(object sender, RoutedEventArgs e)
        {
            _spheroDevice.RunMacro(102, null);
        }
    }
}