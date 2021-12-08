using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using Manager;

namespace SymmetricAlgorithms
{
	public class DES_Symm_Algorithm
	{
		/// <summary>
		/// Function that encrypts the plaintext from inFile and stores cipher text to outFile
		/// </summary>
		/// <param name="inFile"> filepath where plaintext is stored </param>
		/// <param name="outFile"> filepath where cipher text is expected to be stored </param>
		/// <param name="secretKey"> symmetric encryption key </param>
		public static void EncryptFile(string inFile, string outFile, string secretKey, CipherMode mode)
		{
			byte[] header = null;	//image header (54 byte) should not be encrypted
			byte[] body = null;     //image body to be encrypted
            byte[] encryptedBody = null;

            Formatter.Decompose(File.ReadAllBytes(inFile), out header, out body);

            DESCryptoServiceProvider desCryptoProvider = new DESCryptoServiceProvider();
            desCryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(secretKey);
            desCryptoProvider.Mode = mode;
            desCryptoProvider.Padding = PaddingMode.None;

            if (mode.Equals(CipherMode.ECB))
            {
                ICryptoTransform desEncryptTransform = desCryptoProvider.CreateEncryptor();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desEncryptTransform, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(body, 0, body.Length);
                        encryptedBody = memoryStream.ToArray();                              //encrypted image body
                    }
                }
            }
            else if (mode.Equals(CipherMode.CBC))
            {
                desCryptoProvider.GenerateIV();

                ICryptoTransform desEncryptTransform = desCryptoProvider.CreateEncryptor();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desEncryptTransform, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(body, 0, body.Length);

                        encryptedBody = desCryptoProvider.IV.Concat(memoryStream.ToArray()).ToArray();    //encrypted image body with IV	
                    }
                }		
                                
            }

            int outputLenght = header.Length + encryptedBody.Length;                 //header.Length + body.Length
            Formatter.Compose(header, encryptedBody, outputLenght, outFile);
        }


        /// <summary>
        /// Function that decrypts the cipher text from inFile and stores as plaintext to outFile
        /// </summary>
        /// <param name="inFile"> filepath where cipher text is stored </param>
        /// <param name="outFile"> filepath where plain text is expected to be stored </param>
        /// <param name="secretKey"> symmetric encryption key </param>
        public static void DecryptFile(string inFile, string outFile, string secretKey, CipherMode mode)
		{
			byte[] header = null;		//image header (54 byte) should not be decrypted
			byte[] body = null;         //image body to be decrypted
            byte[] decryptedBody = null;

            Formatter.Decompose(File.ReadAllBytes(inFile), out header, out body);

            DESCryptoServiceProvider desCryptoProvider = new DESCryptoServiceProvider();
            desCryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(secretKey);
            desCryptoProvider.Mode = mode;
            desCryptoProvider.Padding = PaddingMode.None;

            if (mode.Equals(CipherMode.ECB))
            {
                ICryptoTransform desDecryptTransform = desCryptoProvider.CreateDecryptor();
                using (MemoryStream memoryStream = new MemoryStream(body))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desDecryptTransform, CryptoStreamMode.Read))
                    {
                        decryptedBody = new byte[body.Length];       //decrypted image body - the same lenght as encrypted part
                        cryptoStream.Read(decryptedBody, 0, decryptedBody.Length);
                    }
                }                
            }
            else if (mode.Equals(CipherMode.CBC))
            {
                desCryptoProvider.IV = body.Take(desCryptoProvider.BlockSize / 8).ToArray();                // take the iv off the beginning of the ciphertext message			
                
                ICryptoTransform desDecryptTransform = desCryptoProvider.CreateDecryptor();

                using (MemoryStream memoryStream = new MemoryStream(body.Skip(desCryptoProvider.BlockSize / 8).ToArray()))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desDecryptTransform, CryptoStreamMode.Read))
                    {
                        decryptedBody = new byte[body.Length - desCryptoProvider.BlockSize / 8];     //decrypted image body - the same lenght as encrypted part
                        cryptoStream.Read(decryptedBody, 0, decryptedBody.Length);
                    }
                }
            }

            int outputLenght = header.Length + decryptedBody.Length;

            Formatter.Compose(header, decryptedBody, outputLenght, outFile);
        }
	}
}
