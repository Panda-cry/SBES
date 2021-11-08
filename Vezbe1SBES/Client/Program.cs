using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            EndpointAddress addres = new EndpointAddress(new Uri("net.tcp://localhost/Contracts/10100"), EndpointIdentity.CreateUpnIdentity("Server"));
            //Credientials.
            IContracts proxy = new ChannelFactory<IContracts>(binding,addres).CreateChannel();

            Console.WriteLine("Name : " + WindowsIdentity.GetCurrent().Name);

            while (true)
            {
                proxy.AddUser(Console.ReadLine(), Console.ReadLine());

            }
        }
    }
}
