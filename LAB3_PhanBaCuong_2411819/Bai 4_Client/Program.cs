using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai_4_Client
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
                EndPoint serverEndpoint = new IPEndPoint(IPAddress.Loopback, 3636);
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                EndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);

                serverSocket.Connect(serverEndpoint);
                byte[] buff = new byte[1024];

                buff = Encoding.UTF8.GetBytes("Connected");

                serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);
                buff = new byte[1024];
                int receivedBytes = serverSocket.ReceiveFrom(buff, ref remoteEndpoint);

                Console.WriteLine("ECHO from " + serverEndpoint.ToString() + " " + Encoding.UTF8.GetString(buff, 0, receivedBytes));

                while (true)
                {

                    buff = Encoding.UTF8.GetBytes(Console.ReadLine());
                    serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);

                    buff = new byte[1024];
                    receivedBytes = serverSocket.ReceiveFrom(buff, ref remoteEndpoint);

                    Console.WriteLine("ECHO from " + serverEndpoint.ToString() + " " + Encoding.UTF8.GetString(buff, 0, receivedBytes));
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
