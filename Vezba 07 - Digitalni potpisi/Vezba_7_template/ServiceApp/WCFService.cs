using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using Manager;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Security;
using System.ServiceModel;

namespace ServiceApp
{
	public class WCFService : IWCFContract
	{
        public void SendMessage(string message, byte[] sign)
        {
            string name = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name) + "_sign";
            X509Certificate2 client = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, name);

            if (DigitalSignature.Verify(message, HashAlgorithm.SHA1, sign, client))
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("Worng");
            }
        }

        public void TestCommunication()
		{
			Console.WriteLine("Communication established.");
		}

		
    }
}
