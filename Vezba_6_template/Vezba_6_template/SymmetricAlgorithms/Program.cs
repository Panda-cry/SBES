using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Manager;
using System.Security.Cryptography;

namespace SymmetricAlgorithms
{
	public class Program
	{
		#region DES Alogirthm

		static void Test_DES_Encrypt(string inputFile, string outputFile, string secretKey, CipherMode mode)
		{
			///Perform DES encryption
		}

		static void Test_DES_Decrypt(string inputFile, string outputFile, string secretKey, CipherMode mode)
		{
			///Perform DES decryption
		}

		#endregion

		#region 3DES Alogirthm

		static void Test_3DES_Encrypt(string inputFile, string outputFile, string secretKey, CipherMode mode)
		{
			_3DES_Symm_Algorithm.EncryptFile(inputFile, outputFile, secretKey, mode);
		}

		static void Test_3DES_Decrypt(string inputFile, string outputFile, string secretKey, CipherMode mode)
		{
			_3DES_Symm_Algorithm.DecryptFile(inputFile, outputFile, secretKey, mode);
		}

		#endregion

		#region AES Alogirthm

		static void Test_AES_Encrypt(string inputFile, string outputFile, string secretKey, CipherMode mode)
		{
			///Perform AES encryption
		}

		static void Test_AES_Decrypt(string inputFile, string outputFile, string secretKey, CipherMode mode)
		{
			///Perform AES decryption
		}

		#endregion

		
		static void Main(string[] args)
		{
            string imgFile = "Penguin.bmp";				//source bitmap file
            string cipherFile = "Ciphered.bmp";			//result of encryption
            string plaintextFile = "Plaintext.bmp";		//result of decryption
            string keyFile = "SecretKey.txt";			//secret key storage

            Console.WriteLine("Symmetric Encryption Example - ECB mode");

			///Generate secret key for appropriate symmetric algorithm and store it to 'keyFile' for further usage
			string eSecretKey = SecretKey.GenerateKey(AlgorithmType.TripleDES);
			SecretKey.StoreKey(eSecretKey, keyFile);
            ///Test_DES_Encrypt(imgFile, cipherFile, eSecretKey, CipherMode.ECB);
            ///Test_AES_Encrypt(imgFile, cipherFile, eSecretKey, CipherMode.ECB);
            Test_3DES_Encrypt(imgFile, cipherFile, eSecretKey, CipherMode.ECB);
            Console.WriteLine("Encryption is done.");
            Console.ReadLine();

            ///Test_DES_Decrypt(cipherFile, plaintextFile, SecretKey.LoadKey(keyFile), CipherMode.ECB);
            ///Test_AES_Decrypt(cipherFile, plaintextFile, SecretKey.LoadKey(keyFile), CipherMode.ECB);
            Test_3DES_Decrypt(cipherFile, plaintextFile, SecretKey.LoadKey(keyFile), CipherMode.ECB);
            Console.WriteLine("Decryption is done.");


            Console.WriteLine("Symmetric Encryption Example - CBC mode");

            ///Generate secret key for appropriate symmetric algorithm and store it to 'keyFile' for further usage


            ///Test_DES_Encrypt(imgFile, cipherFile, eSecretKey, CipherMode.CBC);
            ///Test_AES_Encrypt(imgFile, cipherFile, eSecretKey, CipherMode.CBC);
            ///Test_3DES_Encrypt(imgFile, cipherFile, eSecretKey, CipherMode.CBC);
            Console.WriteLine("Encryption is done.");
			Console.ReadLine();

            ///Test_DES_Decrypt(cipherFile, plaintextFile, SecretKey.LoadKey(keyFile), CipherMode.CBC);
            ///Test_AES_Decrypt(cipherFile, plaintextFile, SecretKey.LoadKey(keyFile), CipherMode.CBC);
            ///Test_3DES_Decrypt(cipherFile, plaintextFile, SecretKey.LoadKey(keyFile), CipherMode.CBC);
            Console.WriteLine("Decryption is done.");

            Console.ReadLine();
		}
	}
}
