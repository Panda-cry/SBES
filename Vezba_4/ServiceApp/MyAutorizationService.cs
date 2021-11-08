using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;

namespace ServiceApp
{
    public class MyAutorizationService : ServiceAuthorizationManager
    {
        public override bool CheckAccess(OperationContext operationContext)
        {
            WindowsIdentity identity = operationContext.ServiceSecurityContext.WindowsIdentity;
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole("Reader");
        }
    }
}
