using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using Manager;
using System.Security.Principal;

namespace ClientApp
{
	public class Program
	{
		static void Main(string[] args)
		{
            /// Define the expected service certificate. It is required to establish cmmunication using certificates.
            string srvCertCN = "wcfservice";

            /// Define the expected certificate for signing ("<username>_sign" is the expected subject name).
            /// .NET WindowsIdentity class provides information about Windows user running the given process
            string signCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name) + "_sign";

            /// Define subjectName for certificate used for signing which is not as expected by the service
            string wrongCertCN = "wrong_sign";

            NetTcpBinding binding = new NetTcpBinding();
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

			/// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
			X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople,
                StoreLocation.LocalMachine, srvCertCN);
			EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/Receiver"),
									  new X509CertificateEndpointIdentity(srvCert));
            
            using (WCFClient proxy = new WCFClient(binding, address))
			{
				/// 1. Communication test
				proxy.TestCommunication();
				Console.WriteLine("TestCommunication() finished. Press <enter> to continue ...");
				Console.ReadLine();

                /// 2. Digital Signing test				
				string message = "Message";
               X509Certificate2 certifiate =  CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, signCertCN);
                /// Create a signature based on the "signCertCN" using SHA1 hash algorithm
                byte[] signed = DigitalSignature.Create(message, HashAlgorithm.SHA1, certifiate);
                proxy.SendMessage(message, signed);

                /// For the same message, create a signature based on the "wrongCertCN" using SHA1 hash algorithm

               
            }
        }
	}
}
