using ServiceContracts;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
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
		
			UserAccountsDB.Add(username, new User(username, password));
			IIdentity identity = Thread.CurrentPrincipal.Identity;
            Console.WriteLine("Tip autentifikacije je  : "+identity.AuthenticationType);

			WindowsIdentity user = identity as WindowsIdentity;
            Console.WriteLine("Ime korisnika koji je pozvao metodu je  :"+ user.Name);
            Console.WriteLine("Sid je :"+user.User);

            Console.WriteLine("Grupe korisnika ");
            foreach (var item in user.Groups)
            {
				SecurityIdentifier sid=(SecurityIdentifier)item.Translate(typeof(SecurityIdentifier));
				string name = sid.Translate(typeof(NTAccount)).ToString();
				Console.WriteLine(name) ;
            }
		}

	}
}
