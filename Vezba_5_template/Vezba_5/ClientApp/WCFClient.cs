using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Contracts;

namespace ClientApp
{
	public class WCFClient : ChannelFactory<IWCFService>, IWCFService, IDisposable
	{
		IWCFService factory;

		public WCFClient(NetTcpBinding binding, EndpointAddress address)
			: base(binding, address)
		{
			factory = this.CreateChannel();
		}

        #region Read()

        public Car Read(int key)
        {
            Car car = null;
            try
            {
                car = factory.Read(key);
                Console.WriteLine("Read allowed");
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error while trying to Read : {0}", e.Detail.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Read : {0}", e.Message);
            }
            return car;

        }

        #endregion

        #region Modify()

        public bool Modify(int key, Car car)
        {
            bool retValue = false;
            try
            {
                retValue = factory.Modify(key, car);
                Console.WriteLine("Modify allowed");
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error while trying to Modify : {0}", e.Detail.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Modify : {0}", e.Message);
            }
            return retValue;
        }


        #endregion

        #region Delete()
        public bool Delete(int key)
        {
            bool retValue = false;
            try
            {
                retValue = factory.Delete(key);
                Console.WriteLine("Delete allowed");
            }
            catch (FaultException<SecurityException> e)
            {
                Console.WriteLine("Error while trying to Delete : {0}", e.Detail.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Delete : {0}", e.Message);
            }
            return retValue;
        }

        #endregion
        #region ManagePrms
        
        public void ManagePermission(bool isAdd, string rolename, params string[] permissions)
        {
            try
            {
                factory.ManagePermission(isAdd, rolename, permissions);
                Console.WriteLine("Manage allowed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Manage : {0}", e.Message);
            }
        }

        public void ManageRoles(bool isAdd, string rolename)
        {
            try
            {
                factory.ManageRoles(isAdd, rolename);
                Console.WriteLine("Manage allowed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Manage : {0}", e.Message);
            }
        }

        #endregion
        public void Dispose()
		{
			if (factory != null)
			{
				factory = null;
			}

			this.Close();
		}

       
    }
}
