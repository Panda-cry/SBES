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
            try
            {
                DES_Symm_Algorithm.EncryptFile(inputFile, outputFile, secretKey, mode);
                Console.WriteLine("The file on location {0} is successfully encrypted.", inputFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Encryption failed. Reason: {0}", e.Message);
            }
        }

		static void Test_DES_Decrypt(string inputFile, string outputFile, string secretKey, CipherMode mode)
		{
            try
            {
                DES_Symm_Algorithm.DecryptFile(inputFile, outputFile, secretKey, mode);
                Console.WriteLine("The file on location {0} is successfully decrypted.", inputFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Decryption failed. Reason: {0}", e.Message);
            }
        }

		#endregion

		#region 3DES Alogirthm

		static void Test_3DES_Encrypt(string inputFile, string outputFile, string secretKey, CipherMode mode)
		{
            try
            {
                TripleDES_Symm_Algorithm.EncryptFile(inputFile, outputFile, secretKey, mode);
                Console.WriteLine("The file on location {0} is successfully decrypted.", inputFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Decryption failed. Reason: {0}", e.Message);
            }
        }

		static void Test_3DES_Decrypt(string inputFile, string outputFile, string secretKey, CipherMode mode)
		{
            try
            {
                TripleDES_Symm_Algorithm.DecryptFile(inputFile, outputFile, secretKey, mode);
                Console.WriteLine("The file on location {0} is successfully decrypted.", inputFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Decryption failed. Reason: {0}", e.Message);
            }
        }

		#endregion

		#region AES Alogirthm

		static void Test_AES_Encrypt(string inputFile, string outputFile, string secretKey, CipherMode mode)
		{
            try
            {
                AES_Symm_Algorithm.EncryptFile(inputFile, outputFile, secretKey, mode);
                Console.WriteLine("The file on location {0} is successfully decrypted.", inputFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Decryption failed. Reason: {0}", e.Message);
            }
        }

		static void Test_AES_Decrypt(string inputFile, string outputFile, string secretKey, CipherMode mode)
		{
            try
            {
                AES_Symm_Algorithm.DecryptFile(inputFile, outputFile, secretKey, mode);
                Console.WriteLine("The file on location {0} is successfully decrypted.", inputFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Decryption failed. Reason: {0}", e.Message);
            }
        }

		#endregion

		
		static void Main(string[] args)
		{
            string imgFile = "../../Penguin.bmp";             //source bitmap file

            string cipherFileCBC = "CipheredCBC.bmp";			//result of ECB encryption
            string plaintextFileCBC = "PlaintextCBC.bmp";		//result of ECB decryption
            string cipherFileECB = "CipheredECB.bmp";			//result of ECB encryption
            string plaintextFileECB = "PlaintextECB.bmp";		//result of ECB decryption

            string keyFile = "SecretKey.txt";     //secret key storage

            string folderNameDES = "DES/";
            string folderName3DES = "TDES/";
            string folderNameAES = "AES/";

            Console.WriteLine("ECB mode");

            ///Generate secret key for appropriate symmetric algorithm and store it to 'keyFile' for further usage
            string eSecretKeyDes = SecretKey.GenerateKey(AlgorithmType.DES);
            SecretKey.StoreKey(eSecretKeyDes, folderNameDES + keyFile);

            string eSecretKeyAes = SecretKey.GenerateKey(AlgorithmType.AES);
            SecretKey.StoreKey(eSecretKeyAes, folderNameAES + keyFile);

            string eSecretKeyTripleDes = SecretKey.GenerateKey(AlgorithmType.TripleDES);
            SecretKey.StoreKey(eSecretKeyTripleDes, folderName3DES + keyFile);

            Test_DES_Encrypt(imgFile, folderNameDES + cipherFileECB, eSecretKeyDes, CipherMode.ECB);
            Test_AES_Encrypt(imgFile, folderNameAES + cipherFileECB, eSecretKeyAes, CipherMode.ECB);
            Test_3DES_Encrypt(imgFile, folderName3DES + cipherFileECB, eSecretKeyTripleDes, CipherMode.ECB);
            Console.WriteLine("Encryption is done.");

            Test_DES_Decrypt(folderNameDES + cipherFileECB, folderNameDES + plaintextFileECB, SecretKey.LoadKey(folderNameDES + keyFile), CipherMode.ECB);
            Test_AES_Decrypt(folderNameAES + cipherFileECB, folderNameAES + plaintextFileECB, SecretKey.LoadKey(folderNameAES + keyFile), CipherMode.ECB);
            Test_3DES_Decrypt(folderName3DES + cipherFileECB, folderName3DES + plaintextFileECB, SecretKey.LoadKey(folderName3DES + keyFile), CipherMode.ECB);
            Console.WriteLine("Decryption is done.");

            Console.WriteLine("CBC mode");

            Test_DES_Encrypt(imgFile, folderNameDES + cipherFileCBC, SecretKey.LoadKey(folderNameDES + keyFile), CipherMode.CBC);
            Test_AES_Encrypt(imgFile, folderNameAES + cipherFileCBC, SecretKey.LoadKey(folderNameAES + keyFile), CipherMode.CBC);
            Test_3DES_Encrypt(imgFile, folderName3DES + cipherFileCBC, SecretKey.LoadKey(folderName3DES + keyFile), CipherMode.CBC);
            Console.WriteLine("Encryption is done.");

            Test_DES_Decrypt(folderNameDES + cipherFileCBC, folderNameDES + plaintextFileCBC, SecretKey.LoadKey(folderNameDES + keyFile), CipherMode.CBC);
            Test_AES_Decrypt(folderNameAES + cipherFileCBC, folderNameAES + plaintextFileCBC, SecretKey.LoadKey(folderNameAES + keyFile), CipherMode.CBC);
            Test_3DES_Decrypt(folderName3DES + cipherFileCBC, folderName3DES + plaintextFileCBC, SecretKey.LoadKey(folderName3DES + keyFile), CipherMode.CBC);
            Console.WriteLine("Decryption is done.");

            Console.ReadLine();
		}
	}
}
