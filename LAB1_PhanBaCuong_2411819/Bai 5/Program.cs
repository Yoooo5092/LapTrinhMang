using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Bai_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ThongTinMayTram();
        }

        static void ThongTinMayTram()
        {
            string hostName = Dns.GetHostName();

            Console.WriteLine(hostName);

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Down) { continue; }

                Console.WriteLine(  "Mo ta: " + nic.Description + "\n" + 
                                    "Ten: " + nic.Name + "\n" + 
                                    "Phan loai: " + nic.NetworkInterfaceType + "\n" + 
                                    "Toc do (Mbps): " + nic.Speed / 1_000_000 + "\n" + 
                                    "MAC: " + nic.GetPhysicalAddress());

                IPInterfaceProperties properties = nic.GetIPProperties();

                foreach (UnicastIPAddressInformation uni in properties.UnicastAddresses)
                {
                    Console.WriteLine((uni.Address.AddressFamily == AddressFamily.InterNetwork ? "IPv4 - " : "IPv6 - ") + uni.Address + "\n");
                }

                Console.WriteLine("================");
            }
        }
    }
}
