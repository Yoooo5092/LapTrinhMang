using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace Bai_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetIPAddress();
        }

        static void GetIPAddress()
        {
            //Bai 2.1
            foreach (NetworkInterface network in NetworkInterface.GetAllNetworkInterfaces())
            {
                //Bai 2.2
                if (network.OperationalStatus != OperationalStatus.Up) continue;

                IPInterfaceProperties properties = network.GetIPProperties();
                Console.WriteLine("MAC Address: " + network.GetPhysicalAddress().ToString());

                foreach (UnicastIPAddressInformation ip in properties.UnicastAddresses)
                {
                    //Bai 2.3
                    Console.WriteLine("Ten card mang: " + network.Name);
                    Console.WriteLine("IP Address: " + ip.Address + (ip.Address.AddressFamily == AddressFamily.InterNetwork ? " | IPv4" : " | IPv6"));

                    Console.WriteLine("Subnet Mask: " + ip.IPv4Mask);

                    foreach (GatewayIPAddressInformation gateway in properties.GatewayAddresses)
                    {
                        if (gateway.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            Console.WriteLine("Default Gateway: " + gateway.Address);
                        }
                    }

                    Console.WriteLine("--------------------------------");
                }
            }
        }
    }
}
