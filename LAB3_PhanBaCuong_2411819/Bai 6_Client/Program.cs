using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai_6_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunUPDClient();
        }

        static void RunUPDClient()
        {
            try
            {
                IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Loopback, 3636);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                int i = 0;
                EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

                while (true) { 
                    string input = Console.ReadLine();
                    
                    socket.SendTo(Encoding.UTF8.GetBytes(input), serverEndpoint);
                    byte[] buff = new byte[i];
                    try
                    {

                        int receivedBytes = socket.ReceiveFrom(buff, 0, buff.Length,SocketFlags.None, ref remote);
                        Console.WriteLine(Encoding.UTF8.GetString(buff, 0, receivedBytes));

                    }
                    catch (SocketException e)
                    {
                        Console.WriteLine(e);
                        i += 10;
                    }
                }

            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}
