using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class CustomPrincipal : IPrincipal
    {
        WindowsIdentity windowsIdentity;

        public CustomPrincipal(WindowsIdentity identity)
        {
            windowsIdentity = identity;
        }

        public IIdentity Identity
        {
            get { return windowsIdentity; }
        }
        public bool IsInRole(string permission)
        {
            foreach(IdentityReference group in windowsIdentity.Groups)
            {
                SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
                var name = sid.Translate(typeof(NTAccount));
                string groupName = Formatter.ParseName(name.ToString());

                if(RolesConfig.GetPermissions(groupName,out string[] permissions))
                {
                    if (permissions.Contains(permission))
                        return true;
                }
               
            }
            return false;
        }
    }
}
