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

            /// Formatter.Decompose();
            if (mode.Equals(CipherMode.ECB))
            {
                DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();

                /// ICryptoTransform desEncrypt = desCrypto.CreateEncryptor();
                /// CryptoStream cryptoStream

                /// output = header + encrypted_body
            }
            else if (mode.Equals(CipherMode.CBC))
            {
                DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
                desCrypto.GenerateIV();     // use generated IV - IV lenght is 'block_size/8'

                ICryptoTransform desEncrypt = desCrypto.CreateEncryptor();
                /// CryptoStream cryptoStream

                /// output = header + encrypted_body_with_IV // Concate
            }
            /// Formatter.Compose(); 
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

            /// Formatter.Decompose();			
            if (mode.Equals(CipherMode.ECB))
            {
                DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
                /// desCrypto.Padding = PaddingMode.None;

                /// ICryptoTransform desDecrypt = desCrypto.CreateDecryptor();
                /// CryptoStream cryptoStream

                /// output = header + decrypted_body
            }
            else if (mode.Equals(CipherMode.CBC))
            {
                DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
                /// desCrypto.IV -> take the IV off the beginning of the ciphertext message			
                desCrypto.Padding = PaddingMode.None;

                ICryptoTransform desDecrypt = desCrypto.CreateDecryptor();
                /// CryptoStream cryptoStream

                /// output = header + decrypted_body
            }
            /// Formatter.Compose();					
        }
	}
}
