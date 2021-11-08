using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace SecurityService
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

			ServiceHost serviceHost = new ServiceHost(typeof(SecurityService));
			serviceHost.AddServiceEndpoint(typeof(ISecurityService), binding, address);
            try
            {
                Console.WriteLine("Who start this app :"+ WindowsIdentity.GetCurrent().Name);
				serviceHost.Open();
                Console.WriteLine("Service Opened");
            }catch(Exception e)
            {
                Console.WriteLine("Erorr occupied ");
            }
          
			Console.ReadLine();
			
		}
	}
}
