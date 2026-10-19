using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Bai_1_Client
{
    public class Program
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
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                
                byte[] buff = new byte[1024];

                while (true)
                {
                    string data = Console.ReadLine();

                    if(string.IsNullOrEmpty(data))
                    {
                        continue;
                    }

                    byte[] dataArray = Encoding.UTF8.GetBytes(data);

                    try
                    {
                        serverSocket.SendTo(dataArray, 0, dataArray.Length, SocketFlags.None, serverEndpoint);
                        if (ExitCheck(data))
                        {
                            break;
                        }

                        EndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);

                        int receivedBytes = serverSocket.ReceiveFrom(buff, ref remoteEndpoint);
                        Console.WriteLine("ECHO from " + remoteEndpoint + " " + Encoding.UTF8.GetString(buff, 0, receivedBytes));
                        buff = new byte[1024];
                    }
                    catch ( SocketException e )
                    {
                        Console.WriteLine("Không tạo được kết nối đến Server, vui lòng thử lại");
                    }
                }

            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }

        }

        static bool ExitCheck(string input)
        {
            if (String.Compare(input, "exit", StringComparison.OrdinalIgnoreCase) == 0 || 
                String.Compare(input, "exit all", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }
            return false;
        }
    }
}
