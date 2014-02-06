using Sphero.Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace ConnectionSample
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private async void DiscoverSpheros()
        {
            try
            {
                // Discover paired Spheros
                List<SpheroInformation> spheroInformations = new List<SpheroInformation>(await SpheroConnectionProvider.DiscoverSpheros());

                if (spheroInformations != null && spheroInformations.Count > 0)
                {
                    // Populate list with Discovered Spheros
                    SpherosDiscovered.ItemsSource = spheroInformations;
                }
                else
                {
                    // No sphero Paired
                    MessageDialog dialogNSP = new MessageDialog("No sphero Paired");
                    await dialogNSP.ShowAsync();
                }

            }
            catch (NoSpheroFoundException)
            {
                MessageDialog dialogNSF = new MessageDialog("No sphero Found");
                dialogNSF.ShowAsync();
            }
            catch (BluetoothDeactivatedException)
            {
                // Bluetooth deactivated
                MessageDialog dialogBD = new MessageDialog("Bluetooth deactivated");
                dialogBD.ShowAsync();
            }
        }

        private async void btnConnection_Click(object sender, RoutedEventArgs e)
        {
            if(SpherosDiscovered.SelectedItem != null)
            {
                SpheroConnection connection = await SpheroConnectionProvider.CreateConnection((SpheroInformation)SpherosDiscovered.SelectedItem);
                Frame.Navigate(typeof(ConnectedPage), connection);
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            DiscoverSpheros();
            base.OnNavigatedTo(e);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DiscoverSpheros();
        }
    }
}
