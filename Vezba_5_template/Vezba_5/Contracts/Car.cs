using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{

    [DataContract]
    public enum Brand {[EnumMember] Audi, [EnumMember] BMW, [EnumMember] Citroen, [EnumMember] Fiat, [EnumMember] Nissan }

    [DataContract]
    public class Car
    {
        Brand brand;
        string model;
        int year;
        int horsepower;

        public Car(Brand brand, string model, int year, int horsepower)
        {
            this.Brand = brand;
            this.Model = model;
            this.Year = year;
            this.Horsepower = horsepower;
        }

        [DataMember]
        public string Model { get => model; set => model = value; }
        [DataMember]
        public int Year { get => year; set => year = value; }
        [DataMember]
        public int Horsepower { get => horsepower; set => horsepower = value; }
        [DataMember]
        internal Brand Brand { get => brand; set => brand = value; }

        public override string ToString()
        {
            return String.Format("Brand : {0}, model : {1}, year : {2}, horsepower : {3}", Brand, Model, Year, Horsepower);
        }
    }
}
