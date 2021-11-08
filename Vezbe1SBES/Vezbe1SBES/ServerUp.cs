using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vezbe1SBES
{
    public class ServerUp
    {
        private ServiceHost serviceHost;

        public ServerUp()
        {
            string endPoint = string.Format("net.tcp://localhost/Contracts/10100");
            serviceHost = new ServiceHost(typeof(ServerImplement));
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            serviceHost.AddServiceEndpoint(typeof(IContracts), binding, endPoint);
            serviceHost.Open();

            IIdentity identity = Thread.CurrentPrincipal.Identity;
            Console.WriteLine("Name : "+WindowsIdentity.GetCurrent().Name);
        }
    }
}
