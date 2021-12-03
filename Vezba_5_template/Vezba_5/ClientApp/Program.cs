using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Contract;

namespace ClientApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9999/WCFService";

            using (WCFClient proxy = new WCFClient(binding, new EndpointAddress(new Uri(address))))
            {
				string aa = proxy.Connect();
            }

			Console.ReadLine();
		}
	}
}
