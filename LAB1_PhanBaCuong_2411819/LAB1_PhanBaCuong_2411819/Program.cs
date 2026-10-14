using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace LAB1_PhanBaCuong_2411819
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string domainName;
			//Bai 1.1
			domainName = Console.ReadLine();
			Console.WriteLine("Phan giai ten mien: " + domainName);
			GetHostInfo(domainName);
			Console.ReadKey();

		}

		static void GetHostInfo(string host)
		{
			try
			{
				//Bai 1.2
				IPHostEntry hostInfo = Dns.GetHostEntry(host);

				Console.WriteLine("Ten mien: " + hostInfo.HostName);

				Console.Write("Dia chi IP: ");
				//Bai 1.3
				foreach (IPAddress ipaddr in hostInfo.AddressList)
				{
					Console.Write(ipaddr.ToString() + " ");
				}
				Console.WriteLine();

			}
			catch (Exception)
			{
				Console.WriteLine("Khong phan giai duoc ten mien: " + host + "\n");
			}
		}

		
	}
}
