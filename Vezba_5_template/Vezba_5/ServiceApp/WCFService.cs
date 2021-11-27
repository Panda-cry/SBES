using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Contracts;
using SecurityManager;

namespace ServiceApp
{
    public class WCFService : IWCFService
    {

        [PrincipalPermission(SecurityAction.Demand, Role = "Delete")]
        public bool Delete(int key)
        {           
            return Database.cars.Remove(key);               
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Administrate")]
        public void ManagePermission(bool isAdd, string rolename, params string[] permissions)
        {
            if (isAdd) // u pitanju je dodavanje
            {
                RolesConfig.AddPermissions(rolename, permissions);
            }
            else // u pitanju je brisanje
            {
                RolesConfig.RemovePermissions(rolename, permissions);
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Administrate")]
        public void ManageRoles(bool isAdd, string rolename)
        {
            if (isAdd) // u pitanju je dodavanje
            {
                RolesConfig.AddRole(rolename);
            }
            else // u pitanju je brisanje
            {
                RolesConfig.RemoveRole(rolename);
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Modify")]
        public bool Modify(int key, Car car)
        {
            if (Database.cars.ContainsKey(key))
            {
                Database.cars[key] = car;
                return true;
            }
            return false;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Read")]
        public Car Read(int key)
        {
            if (Database.cars.ContainsKey(key))
            {
                return Database.cars[key];
            }
            return null;           

        }
    }
}
