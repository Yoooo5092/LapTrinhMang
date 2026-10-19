using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai_5_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunUDPClient();
        }

        static void RunUDPClient()
        {
            try
            {
                IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Loopback, 3636);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
                byte[] buff = new byte[1024];

                for (int i = 0; i < 5; i++)
                {
                    buff = Encoding.UTF8.GetBytes("Thông điệp " + i);

                    socket.SendTo(buff, 0, buff.Length, SocketFlags.None, serverEndpoint);


                }
                Console.ReadLine();

            }

            catch (SocketException e) {
                Console.WriteLine(e);
                Console.ReadLine();
            }

        }
    }
}
