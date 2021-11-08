using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public enum Brand {[EnumMember] Audi,[EnumMember] BMW, [EnumMember] Citroen, [EnumMember] Fiat, [EnumMember] Nissan}
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
        public Brand Brand { get => brand; set => brand = value; }
        [DataMember]
        public int Year { get => year; set => year = value; }
        [DataMember]
        public int Horsepower { get => horsepower; set => horsepower = value; }

        public override string ToString()
        {
            return string.Format("Model : {0}  Brand : {1} Year : {2} Horsepower : {3} ", model, brand, year, horsepower);
        }
    }
}
