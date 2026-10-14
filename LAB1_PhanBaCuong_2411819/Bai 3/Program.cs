using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Bai_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();
            Console.WriteLine(ParseEndPoint(str));
        }

        public static string[] SplitEndPoint(string raw)
        {
            try
            {
                string[] splitedRaw = raw.Split(':');
                if (splitedRaw.Length != 2)
                {
                    return null;
                }
                return splitedRaw;
            }
            catch
            {
                return null;
            }
        }

        public static IPEndPoint ParseEndPoint(string raw)
        {
            IPAddress address; int port;
            string[] splitedRaw = SplitEndPoint(raw);
            if (splitedRaw == null) { return null; }

            try
            {
                int rawPort = int.Parse(splitedRaw[1]);
                if (rawPort > 65535 || rawPort < 1) { return null; }

                else { 
                    port = rawPort;
                }

                if (!IPAddress.TryParse(splitedRaw[0], out address))
                {
                    IPHostEntry entry = Dns.GetHostEntry(splitedRaw[0]);

                    address = entry.AddressList[0];
                }

            }
            catch
            {
                return null;
            }
            return new IPEndPoint(address, port);
        }
    }
}
