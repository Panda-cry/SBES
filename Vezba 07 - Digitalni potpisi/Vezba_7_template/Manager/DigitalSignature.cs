using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace Manager
{
    public enum HashAlgorithm { SHA1, SHA256}
	public class DigitalSignature
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"> a message/text to be digitally signed </param>
        /// <param name="hashAlgorithm"> an arbitrary hash algorithm </param>
        /// <param name="certificate"> certificate of a user who creates a signature </param>
        /// <returns> byte array representing a digital signature for the given message </returns>
        public static byte[] Create(string message, HashAlgorithm hashAlgorithm, X509Certificate2 certificate)
        {
            //TO DO

            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)certificate.PrivateKey;
            if (csp == null)
            {
                throw new Exception("Valid certificate not found");
            }
            byte[] signature = null;
            byte[] byteMessage = null;
            byte[] hash = null;
            UnicodeEncoding code = new UnicodeEncoding();
            byteMessage = code.GetBytes(message);

            if (hashAlgorithm.Equals(HashAlgorithm.SHA1))
            {
                SHA1Managed sha = new SHA1Managed();
                hash = sha.ComputeHash(byteMessage);
            }
            else
            {
                SHA256Managed sha = new SHA256Managed();
                hash = sha.ComputeHash(byteMessage);
            }
            /// Looks for the certificate's private key to sign a message
            
            

            // Use RSACryptoServiceProvider support to create a signature using a previously created hash value
          
            signature = csp.SignData(hash, CryptoConfig.MapNameToOID(hashAlgorithm.ToString()));

            return signature;
        }


		public static bool Verify(string message, HashAlgorithm hashAlgorithm, byte[] signature, X509Certificate2 certificate)
		{
            //TO DO

            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)certificate.PublicKey.Key;
            /// Looks for the certificate's public key to verify a message
            UnicodeEncoding code = new UnicodeEncoding();

            byte[] messageByte = code.GetBytes(message);
            byte[] hash = null;

            if (hashAlgorithm.Equals(HashAlgorithm.SHA1))
            {
                SHA1Managed sha = new SHA1Managed();
                hash = sha.ComputeHash(messageByte);
            }
            else
            {
                SHA256Managed sha = new SHA256Managed();
                hash = sha.ComputeHash(messageByte);
            }
            /// Use RSACryptoServiceProvider support to compare two - hash value from signature and newly created hash value

            return csp.VerifyHash(hash,CryptoConfig.MapNameToOID(hashAlgorithm.ToString()),signature);
        }
	}
}
