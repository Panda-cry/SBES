using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Contracts;

namespace ClientApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:8888/WCFService";

			using (WCFClient proxy = new WCFClient(binding, new EndpointAddress(new Uri(address))))
			{
                Console.WriteLine("Read car  1 : ");
				Car car = proxy.Read(1);
                try
                {
					Console.WriteLine(car.ToString());
				}catch(Exception)
                {
                    Console.WriteLine("NULL");
                }
                
                Console.WriteLine(" ------------------------------  ");
                Console.WriteLine("Modify first car : ");
				Car temp = new Car(Brand.Audi, "A55", 2000, 199);
				proxy.Modify(1, temp);
				Console.WriteLine(" ------------------------------  ");
				Console.WriteLine("Read car  1 : ");
				car = proxy.Read(1);
				try
				{
					Console.WriteLine(car.ToString());
				}
				catch (Exception)
				{
					Console.WriteLine("NULL");
				}
				Console.WriteLine(" ------------------------------  ");

				Console.WriteLine("Deletiing first  car :");
				proxy.Delete(1);
				Console.WriteLine(" ------------------------------  ");

				try
                {
					Console.WriteLine("Read car  1 : ");
					car = proxy.Read(1);
					Console.WriteLine(car.ToString());
				}catch(Exception e)
                {
                    Console.WriteLine("Null operation");
                }
				
			}

			Console.ReadLine();
		}
	}
}
