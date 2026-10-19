using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Bai1
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunUDPServer();
        }

        static void RunUDPServer()
        {
            IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Any, 3636);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                serverSocket.Bind(serverEndpoint);
                Console.WriteLine("Server Online");

                byte[] buff = new byte[1024];

                while (true)
                {
                    EndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);

                    int receivedBytes = serverSocket.ReceiveFrom(buff, ref remoteEndpoint);
                    if (receivedBytes == 0)
                    {
                        continue;
                    }
                    IPEndPoint clientEndpoint = (IPEndPoint)remoteEndpoint;
                    string data = Encoding.UTF8.GetString(buff, 0, receivedBytes);

                    if (CheckExit(data))
                    {
                        break;
                    }
                    Console.WriteLine(remoteEndpoint + " " + data);

                    serverSocket.SendTo(buff, remoteEndpoint);
                    buff = new byte[1024];
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }


        }

        static bool CheckExit(string input)
        {
            if (String.Compare(input, "exit all", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }
            return false;
        }
    }
}
