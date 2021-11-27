using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Contracts;

namespace ClientApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9999/WCFService";
			
        

            using (WCFClient proxy = new WCFClient(binding, new EndpointAddress(new Uri(address))))
			{
				Car car = proxy.Read(1);
                Console.WriteLine(car.Brand + car.Model + car.Horsepower);
				proxy.Modify(1, new Car(Brand.BMW, "E40", 2000, 200));
				Car newCar = proxy.Read(1);
                Console.WriteLine(newCar.Brand + newCar.Model + newCar.Horsepower);

				proxy.Delete(1);
				Car newCar1 = proxy.Read(1);
				if(newCar1 != null)
                {
                    try
                    {
						Console.WriteLine(newCar1.Brand + newCar1.Model + newCar1.Horsepower);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Null");
                    }
                }
				
			}

			Console.ReadLine();
		}
	}
}
