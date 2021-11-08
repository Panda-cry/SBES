using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Contracts;

namespace ServiceApp
{
    public class WCFService : IWCFService
    {
        //[PrincipalPermission(SecurityAction.Demand,Role ="Admin")]
        public bool Delete(int key)
        {
            if (Thread.CurrentPrincipal.IsInRole("Admin"))
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
                SecurityException se = new SecurityException();
                se.Message = string.Format("Not allowed  user {0} at time {1},  in method Admin ", Thread.CurrentPrincipal.Identity.Name, DateTime.Now);
                throw new FaultException<SecurityException>(se, new FaultReason(se.Message));
            }
            ;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "Modify")]
        public bool Modify(int key, Car car)
        {
            if (Thread.CurrentPrincipal.IsInRole("Modify"))
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
                SecurityException se = new SecurityException();
                se.Message = string.Format("Not allowed  user {0} at time {1},  in method Modify ", Thread.CurrentPrincipal.Identity.Name, DateTime.Now);
                throw new FaultException<SecurityException>(se, new FaultReason(se.Message));
            }
           
        }
        //[PrincipalPermission(SecurityAction.Demand,Role ="Reader")]
        public Car Read(int key)
        {
            if (Thread.CurrentPrincipal.IsInRole("Reader"))
            {
                if (Database.cars.ContainsKey(key))
                    return Database.cars[key];
                return null;
            }
            else
            {
                SecurityException se = new SecurityException();
                se.Message = string.Format("Not allowed  user {0} at time {1},  in method Read ", Thread.CurrentPrincipal.Identity.Name, DateTime.Now);
                throw new FaultException<SecurityException>(se,new FaultReason(se.Message));
            }
            
        }
    }
}
