using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IMUSample.Resources;
using Sphero.Communication;

namespace IMUSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            DiscoverSpheros();
            // Exemple de code pour la localisation d'ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void DiscoverSpheros()
        {
            try
            {
                // Discover paired Spheros
                IEnumerable<SpheroInformation> spheroInformations = await SpheroConnectionProvider.DiscoverSpheros();

                if (spheroInformations != null && spheroInformations.Any())
                {
                    // Populate list with Discovered Spheros
                    SpherosDiscovered.ItemsSource = spheroInformations;
                }
                else
                {
                    // No sphero Paired
                    MessageBox.Show("No sphero Paired");
                }
            }

            catch (BluetoothDeactivatedException)
            {
                // Bluetooth deactivated
                MessageBox.Show("Bluetooth deactivated");
            }
        }

        private async void btnConnection_Click(object sender, RoutedEventArgs e)
        {
            if (SpherosDiscovered.SelectedItem != null)
            {
                SpheroInformation information = (SpheroInformation)SpherosDiscovered.SelectedItem;
                SpheroConnection connection = await SpheroConnectionProvider.CreateConnection(information);

                if (connection == null)
                {
                    MessageBox.Show("Connection failed");
                }
                else
                {
                    App.CurrentConnection = connection;
                    NavigationService.Navigate(new Uri("/CalibrationPage.xaml", UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DiscoverSpheros();
        }

        // Exemple de code pour la conception d'une ApplicationBar localisée
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Définit l'ApplicationBar de la page sur une nouvelle instance d'ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crée un bouton et définit la valeur du texte sur la chaîne localisée issue d'AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crée un nouvel élément de menu avec la chaîne localisée d'AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}