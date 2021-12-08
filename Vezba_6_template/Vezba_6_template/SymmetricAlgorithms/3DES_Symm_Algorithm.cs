using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Manager;
using System.IO;

namespace SymmetricAlgorithms
{
	public class _3DES_Symm_Algorithm
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


            TripleDESCryptoServiceProvider _3desCrypto = new TripleDESCryptoServiceProvider
            {
                Key = ASCIIEncoding.ASCII.GetBytes(secretKey),
                Padding = PaddingMode.None,
                Mode = mode
            };

            /// Formatter.Decompose();
            if (mode.Equals(CipherMode.ECB))
            {
                

                 ICryptoTransform _3desEncrypt = _3desCrypto.CreateEncryptor();
                using(MemoryStream ms =  new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, _3desEncrypt, CryptoStreamMode.Write))
                    {
                        cs.Write(body, 0, body.Length);
                        encryptedBody = ms.ToArray();
                    }
                }
                /// CryptoStream cryptoStream

                /// output = header + encrypted_body
            }
            else if (mode.Equals(CipherMode.CBC))
            {
                _3desCrypto.GenerateIV();       // use generated IV - IV lenght is 'block_size/8'


                ICryptoTransform _3desEncrypt = _3desCrypto.CreateEncryptor();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, _3desEncrypt, CryptoStreamMode.Write))
                    {
                        cs.Write(body, 0, body.Length);
                        encryptedBody = ms.ToArray();
                    }
                }

                /// output = header + encrypted_body_with_IV / Concate
            }
            Formatter.Compose(header, encryptedBody, header.Length + encryptedBody.Length, outFile);
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
            byte[] plainBody = null;

             Formatter.Decompose(File.ReadAllBytes(inFile),out header,out body);

            TripleDESCryptoServiceProvider desCrypto = new TripleDESCryptoServiceProvider
            {
                Key = ASCIIEncoding.ASCII.GetBytes(secretKey),
                Mode = mode,
                Padding = PaddingMode.None
            };

            if (mode.Equals(CipherMode.ECB))
            {
               
                /// _3desCrypto.Padding = PaddingMode.None;

                 ICryptoTransform _3desDecrypt = desCrypto.CreateDecryptor();

                using(MemoryStream ms = new MemoryStream(body))
                {
                    using(CryptoStream cs = new CryptoStream(ms, _3desDecrypt, CryptoStreamMode.Read))
                    {
                        plainBody = new byte[body.Length];
                        cs.Read(plainBody, 0, plainBody.Length);
                    }
                }
                /// CryptoStream cryptoStream

                /// output = header + decrypted_body
            }
            else if (mode.Equals(CipherMode.CBC))
            {

                /// _3desCrypto.IV -> take the IV off the beginning of the ciphertext message			
                desCrypto.IV = body.Take(desCrypto.BlockSize / 8).ToArray();
                ICryptoTransform _3desDecrypt = desCrypto.CreateDecryptor();

                using (MemoryStream ms = new MemoryStream(body))
                {
                    using (CryptoStream cs = new CryptoStream(ms, _3desDecrypt, CryptoStreamMode.Read))
                    {
                        plainBody = new byte[body.Length];
                        cs.Read(plainBody, 0, plainBody.Length);
                    }
                }
                /// output = header + decrypted_body
            }
             Formatter.Compose(header,plainBody,header.Length + plainBody.Length,outFile);
        }
	}
}
