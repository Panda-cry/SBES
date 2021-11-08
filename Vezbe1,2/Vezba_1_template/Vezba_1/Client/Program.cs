using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	public class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9999/SecurityService";

			binding.Security.Mode = SecurityMode.Transport;
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
			binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

			EndpointAddress endpoint = new EndpointAddress(new Uri(address), EndpointIdentity.CreateUpnIdentity("TUF"));

			using (ClientProxy proxy = new ClientProxy(binding, endpoint))
			{
                //Prve Vezbe
                //            Console.WriteLine("Unesite username i password : ");
                //string username = Console.ReadLine();
                //string password = Console.ReadLine();
                //proxy.AddUser(username, password);
                //            Console.WriteLine("Who started this app :"+WindowsIdentity.GetCurrent().Name);

                //Druge Vezbe
                Console.WriteLine("I will try to make new file");
				proxy.CreateFileWithIpersonificiation("PeraWithIdentification.txt");
			}

			Console.ReadLine();
		}
	}
}
