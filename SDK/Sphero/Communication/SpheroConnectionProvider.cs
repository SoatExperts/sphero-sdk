using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Networking.Proximity;


#if NETFX_CORE
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
#endif

namespace Sphero.Communication
{
    /// <summary>
    /// Provide methods like discover sphero, or create connection
    /// </summary>
    public static class SpheroConnectionProvider
    {
        /// <summary>
        /// Bluetooth SDP ID of Sphero devices
        /// </summary>
        public const string SERVICE_ID = "00001101-0000-1000-8000-00805F9B34FB";

        public static async Task<IEnumerable<SpheroInformation>> DiscoverSpheros()
        {
            try
            {
#if NETFX_CORE

                DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));
                

                if (deviceCollection == null)
                    return null;

                List<SpheroInformation> informations = new List<SpheroInformation>();

                foreach (DeviceInformation device in deviceCollection)
                {
                    RfcommDeviceService service = await RfcommDeviceService.FromIdAsync(device.Id);
                    informations.Add(new SpheroInformation(device.Name, service.ConnectionHostName));
                }

                return informations;
#else
            PeerFinder.AllowBluetooth = true;
            PeerFinder.AllowWiFiDirect = false;
            //PeerFinder.AlternateIdentities["Bluetooth:SDP"] = SpheroConnectionProvider.SERVICE_ID;
            PeerFinder.AlternateIdentities["Bluetooth:PAIRED"] = "";
            IReadOnlyList<PeerInformation> peers = await PeerFinder.FindAllPeersAsync();

            return peers.Where(p => p.DisplayName.ToLower().StartsWith("sphero")).Select(s => new SpheroInformation(s.DisplayName, s.HostName));
#endif
            }
            catch(Exception ex)
            {
                if (ex.HResult == -2147023729)
                {
                    throw new BluetoothDeactivatedException(ex);
                }

                throw new NoSpheroFoundException(ex);
            }
        }

        public static async Task<SpheroConnection> CreateConnection(SpheroInformation spheroInformations)
        {
            try
            {
                SpheroConnection connection = new SpheroConnection(spheroInformations);

                if (await connection.Connect())
                    return connection;
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147023729)
                {
                    throw new BluetoothDeactivatedException(ex);
                }

                throw new NoSpheroFoundException(ex);
            }
        }

        
    }
}
