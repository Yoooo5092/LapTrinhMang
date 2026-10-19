using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Bai_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunSocketServer();
        }

        static void RunSocketServer()
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 3636);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(serverEndPoint);
            serverSocket.Listen(10);

            Console.WriteLine("Server Online!");
            while (true)
            {

                Socket clientSocket = serverSocket.Accept();

                EndPoint clientEndPoint = clientSocket.RemoteEndPoint;

                Console.WriteLine(clientEndPoint.ToString());

                if (clientSocket.Connected)
                {
                    SocketSend(clientSocket, "Hello World!");

                    while (true)
                    {
                        try
                        {
                            string received = SocketReceive(clientSocket);

                            Console.WriteLine(received);

                            SocketSend(clientSocket, clientEndPoint.ToString() + " noi " + received);

                        }
                        catch (SocketException e) { Console.WriteLine("Client ngat ket noi");  break; }
                        
                    }
                }
            }
        }

        public static string SocketReceive(Socket socket)
        {
            byte[] buffer = new byte[1024];
            int bytesReceived = socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);

            if (bytesReceived > 0) { return Encoding.ASCII.GetString(buffer, 0, bytesReceived); }

            else { return null; }
        }

        public static void SocketSend(Socket socket, string data)
        {
            byte[] buffer;

            buffer = Encoding.ASCII.GetBytes(data);
            socket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
    }
}
