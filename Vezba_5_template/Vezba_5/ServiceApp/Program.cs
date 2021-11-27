﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using Contracts;
using System.IdentityModel.Policy;
using SecurityManager;

namespace ServiceApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9999/WCFService";

			ServiceHost host = new ServiceHost(typeof(WCFService));
			host.AddServiceEndpoint(typeof(IWCFService), binding, address);

            // podesavamo da se koristi MyAuthorizationManager umesto ugradjenog
            host.Authorization.ServiceAuthorizationManager = new CustomAuthorizationManager();

			// TO DO : podesesiti custom polisu, odnosno nas objekat principala           
			host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;

			List<IAuthorizationPolicy> polices = new List<IAuthorizationPolicy>();
			polices.Add(new CustomAuthorizationPolicy());

			host.Authorization.ExternalAuthorizationPolicies = polices.AsReadOnly();


            host.Open();
			Console.WriteLine("WCFService is opened. Press <enter> to finish...");
			Console.ReadLine();

			host.Close();
		}
	}
}
