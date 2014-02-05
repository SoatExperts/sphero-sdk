using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ConnectionSample
{
    public partial class ConnectedPage : PhoneApplicationPage
    {
        public ConnectedPage()
        {
            InitializeComponent();

            if (App.CurrentConnection != null)
            {
                MessageBox.Show(string.Format("Connected to {0}", App.CurrentConnection.BluetoothName));
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
    }
}