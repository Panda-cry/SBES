using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vezbe1SBES
{
    public class ServerImplement : IContracts
    {
        private static Dictionary<string, User> korisnici = new Dictionary<string, User>();
        public void AddUser(string username, string password)
        {
            if (korisnici.ContainsKey(username))
            {
                Console.WriteLine("We already posses that user !!!");
                return;
            }
            korisnici.Add(username, new User(username, password));

            IIdentity identity = Thread.CurrentPrincipal.Identity;
            Console.WriteLine($"\t Type :  {identity.AuthenticationType}");
            Console.WriteLine($"\t Name : {identity.Name}");

            WindowsIdentity ident = identity as WindowsIdentity;
            Console.WriteLine("SID : " + ident.User);

            foreach (var item in ident.Groups)
            {
                SecurityIdentifier sid = (SecurityIdentifier)item.Translate(typeof(SecurityIdentifier));
                string name = sid.Translate(typeof(NTAccount)).ToString();
                Console.WriteLine(name);
            }
        }
    }
}
