using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Contracts;


namespace ServiceApp
{
    public class WCFService : IWCFService
    {
        //[PrincipalPermission(SecurityAction.Demand, Role = "admin")]
        public bool Delete(int key)
        {
            if (Thread.CurrentPrincipal.IsInRole("admin"))
            {
                if (Database.cars.ContainsKey(key))
                {
                    Database.cars.Remove(key);
                    return true;
                }
                return false;
            }
            else
            {
                SecurityException ss = new SecurityException();
                ss.Message = string.Format($"User : {0} tried to call method Delete User need to be: admin role! + {1}",Thread.CurrentPrincipal.Identity.Name,DateTime.Now);
                throw new FaultException<SecurityException>(ss);
            }
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "modifier")]
        public bool Modify(int key, Car car)
        {
            IIdentity identity = Thread.CurrentPrincipal.Identity;
            if (Thread.CurrentPrincipal.IsInRole("modifier"))
            {
                if (Database.cars.ContainsKey(key))
                {
                    Database.cars[key] = car;
                    return true;
                }
                return false;
            }
            else
            {
                Contracts.SecurityException ss = new Contracts.SecurityException();
                ss.Message = string.Format($"User : {0} tried to call method Modify User need to be: modifier role! + {1}", identity.Name, DateTime.Now);
                throw new FaultException<SecurityException>(ss);
            }
            
        }
        //[PrincipalPermission(SecurityAction.Demand,Role ="reader")]
        public Car Read(int key)
        {
            if (Thread.CurrentPrincipal.IsInRole("reader"))
            {
                if (Database.cars.ContainsKey(key))
                {
                    return Database.cars[key];
                }
                return null;
            }
            else
            {
                Contracts.SecurityException ss = new Contracts.SecurityException();
                ss.Message = string.Format($"User : {0} tried to call method Read User need to be: reader role! + {1}", Thread.CurrentPrincipal.Identity.Name, DateTime.Now);
                throw new FaultException<SecurityException>(ss);
            }
           
            
        }
    }
}
