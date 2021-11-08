using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientProxy : ChannelFactory<ISecurityService>, ISecurityService, IDisposable
    {
        ISecurityService factory;

        public ClientProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            this.Credentials.Windows.AllowNtlm = false;
            factory = this.CreateChannel();
        }
        public ClientProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            //this.Credentials.Windows.AllowNtlm = false;
            this.Credentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            factory = this.CreateChannel();
        }

        public void AddUser(string username, string password)
        {

            try
            {
                factory.AddUser(username, password);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public void CreateFile(string fileName)
        {
            try
            {
                factory.CreateFile(fileName);
            }catch(SecurityException ex)
            {
                Console.WriteLine(ex.Message) ;
            }
        }

        public void CreateFileWithIpersonificiation(string fileName)
        {
            try
            {
                factory.CreateFileWithIpersonificiation(fileName);
            }
            catch (SecurityException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
