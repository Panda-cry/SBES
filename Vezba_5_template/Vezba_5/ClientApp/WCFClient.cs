using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Contract;

namespace ClientApp
{
	public class WCFClient : ChannelFactory<IServiceManager>, IServiceManager, IDisposable
	{
		IServiceManager factory;

		public WCFClient(NetTcpBinding binding, EndpointAddress address)
			: base(binding, address)
		{
			factory = this.CreateChannel();
		}

      
        public void Dispose()
		{
			if (factory != null)
			{
				factory = null;
			}

			this.Close();
		}

        public string Connect()
        {
            throw new NotImplementedException();
        }

        public bool StartNewService(string encryptedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
