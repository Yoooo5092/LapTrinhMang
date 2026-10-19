using System.Net.Sockets;
using System.Net;
using System;
using System.Runtime.Remoting.Channels;
using System.Text;

namespace Bai_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunSocketClient();
        }

        static void RunSocketClient()
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 3636);

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            bool isRunning = true;
            while (isRunning)
            {
                try
                {
                    Console.WriteLine("Dang tao ket noi toi " + serverEndPoint);
                    serverSocket.Connect(serverEndPoint);
                    serverSocket.ReceiveTimeout = 5000;

                    Console.WriteLine("Ket noi thanh cong");
                    byte[] buffer;

                    if (serverSocket.Connected)
                    {
                        string received = SocketReceive(serverSocket);
                        Console.WriteLine("Thong tin tu server: " + received);

                        while (true)
                        {
                            string data = Console.ReadLine();
                            if (string.IsNullOrEmpty(data)) continue;
                            if (CheckExit(data)) { isRunning = false; break; };

                            buffer = Encoding.ASCII.GetBytes(data);

                            serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);

                            Console.WriteLine(SocketReceive(serverSocket));

                        }
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Không thể kết nối đến server!, Đang thử lại...");

                }

            }
        }

        public static bool CheckExit(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return false;
            if (string.Compare(msg, "exit", StringComparison.OrdinalIgnoreCase) == 0) return true;
            return false;
        }
        public static string SocketReceive(Socket socket)
        {
            byte[] buffer = new byte[1024];
            int bytesReceived = socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);

            if (bytesReceived > 0) { return Encoding.ASCII.GetString(buffer, 0, bytesReceived); }
            return null;
        }
    }
}
