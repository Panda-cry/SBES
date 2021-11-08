using ServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityService
{
	public class SecurityService : ISecurityService
	{
		public static Dictionary<string, User> UserAccountsDB = new Dictionary<string, User>();

		/// <summary>
		/// Add new user to UserAccountsDB. Dictionary Key is "username"
		/// </summary>
		public void AddUser(string username, string password)
		{
            if (!UserAccountsDB.ContainsKey(username))
            {
				UserAccountsDB.Add(username, new User(username, password));
                Console.WriteLine("User is added");
            }
            else
            {
                Console.WriteLine("Vec imamo nekoga sa tim username");
            }

            Console.WriteLine("Informations about user whoo call this function :");
            // Moje zapazanje windows identity nam daje koji tred je zapoceo instancu i daje nam njegove osobine tj poljaa koja se nalaze
            // da bi dobili nesto tj ko je zvao moramo da iz treda izvucemo currentPrincipal tj svaka app koja se dize je neki tread
            // u njemu se skrivaju ta polja sad uzimamo identitet pa ga castujemo mozemo i bez casta jer daju slicne infos 
            //windows koristiomo kada ocemo tred koji je digao da ispisemo, a thread pa identity koristimo kada zelimo da vidimo infos od ovoga sto je 
            //pozvao  metodu 
			IIdentity identity = Thread.CurrentPrincipal.Identity;
			WindowsIdentity winIdent = identity as WindowsIdentity;
            Console.WriteLine("Name of user is : " +winIdent.Name);
            Console.WriteLine("Sid of user is : " +winIdent.User);
            Console.WriteLine("Protocol that user usies : "+winIdent.AuthenticationType);
            Console.WriteLine("Something about groups fom user : ");
            foreach (var item in winIdent.Groups)
            {
                string name = ((NTAccount)item.Translate(typeof(NTAccount))).ToString();
                Console.WriteLine(name);
            }
            Console.WriteLine("----------------------------------------------");

		}

        public void CreateFile(string fileName)
        {
            StreamWriter sw = null;
            try
            {
                sw = File.CreateText(fileName);
                
                Console.WriteLine("Succesfully maked ");
            }catch(Exception e)
            {
                throw new FaultException<SecurityException>( new SecurityException(e.Message));
            }
            finally
            {
                sw.Close();
                Console.WriteLine("StreamWriter is closed");
            }
        }

        public void CreateFileWithIpersonificiation(string fileName)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            WindowsIdentity winIdentity = identity as WindowsIdentity;

            using (winIdentity.Impersonate())
            {
                StreamWriter sw = null;
                try
                {
                    sw = File.CreateText(fileName);

                    Console.WriteLine("Succesfully maked ");
                }
                catch (Exception e)
                {
                    throw new FaultException<SecurityException>(new SecurityException(e.Message));
                }
                finally
                {
                    sw.Close();
                    Console.WriteLine("StreamWriter is closed");
                }
            }

        }
    }
}
