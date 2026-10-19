using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai_5_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunUDPServer();
        }

        static void RunUDPServer()
        {
            try
            {
                IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Any, 3636);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                
                socket.Bind(serverEndpoint);
                Console.WriteLine("Server Online!");
                byte[] buff = new byte[1024];

                while (true)
                {
                    EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

                    for (int i = 0; i < 5; i++)
                    {
                        int receivedBytes = socket.ReceiveFrom(buff,  ref remote);
                        Console.WriteLine(Encoding.UTF8.GetString(buff, 0, receivedBytes));

                        socket.SendTo(buff, receivedBytes, SocketFlags.None ,remote);
                    }

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
