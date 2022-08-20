using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace MDevoldere.Net
{
    public class LocalNetwork
    {
        public static readonly List<LocalNetwork> Items = new();

        public static void GetIPv4Addresses()
        {
            Items.Clear();

            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel
                    && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback
                    && ni.OperationalStatus == OperationalStatus.Up
                    && ni.Name != "vEthernet (Default Switch)"
                    )
                {
                    foreach (var ua in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ua.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            Items.Add(new LocalNetwork(ni.Name, ua.Address.ToString()));
                        }
                    }
                }
            }

            if(Items.Count < 1)
            {
                throw new Exception("No IPv4 Address found !");
            }
        }

        static LocalNetwork()
        {
            GetIPv4Addresses();
        }

        public string Name { get; private set; }
        public string Address { get; private set; }

        public LocalNetwork(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return Name + " " + Address;
        }

    }


}
