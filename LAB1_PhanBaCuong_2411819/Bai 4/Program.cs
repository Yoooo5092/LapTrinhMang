using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace Bai_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IPAddress: ");
            string address = Console.ReadLine();

            Console.WriteLine("SubnetMask: ");
            string subnetMask = Console.ReadLine();

            Console.WriteLine(GetNetworkAddress(address, subnetMask));

        }

        static IPAddress CheckIPAddress(string raw)
        {
            try
            {
                return IPAddress.Parse(raw);
            }
            catch
            {
                return null;
            }
        }

        static IPAddress GetNetworkAddress(string rawAddress, string rawMask)
        {
            IPAddress address = CheckIPAddress(rawAddress);
            IPAddress mask = CheckIPAddress(rawMask);

            if ( address == null || mask == null)
            {
                return null;
            }

            Console.WriteLine("--result--");

            Console.WriteLine(address);
            Console.WriteLine(mask);

            byte[] addressBytes = address.GetAddressBytes();
            byte[] maskBytes = mask.GetAddressBytes();
            byte[] networkAddressBytes = BitwiseAnd(addressBytes, maskBytes);

            return new IPAddress(networkAddressBytes);
        }

        static byte[] BitwiseAnd(byte[] addressBytes, byte[] maskBytes)
        {
            byte[] result = new byte[4];
            if (addressBytes.Length != 4 || maskBytes.Length != 4)
            {
                return null;
            }

            for (int i = 0; i < addressBytes.Length; i++)
            {
                result[i] = (byte)(addressBytes[i] & maskBytes[i]); 
            }
            return result;
        }
    }
}
