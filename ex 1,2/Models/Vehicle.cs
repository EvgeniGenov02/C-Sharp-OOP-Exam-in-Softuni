using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand= brand;
            Model= model;
            MaxMileage= maxMileage;
            LicensePlateNumber= licensePlateNumber;
            BatteryLevel = 100;
        }
        private string brand;

        public string Brand
        {
            get { return brand; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BrandNull);
                }
                brand = value;
            }
        }
        private string model;

        public string Model
        {
            get { return model; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNull);
                }
                model = value;
            }
        }
        public double MaxMileage { get; }

        private string licensePlateNumber;

        public string LicensePlateNumber
        {
            get { return licensePlateNumber; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
                }
                licensePlateNumber = value;
            }
        }

        public int BatteryLevel { get; private set; }

        public bool IsDamaged { get; private set; }
        public void Drive(double mileage)
        {
            double percent =(mileage / MaxMileage )* 100;
            if (this.GetType().Name == "CargoVan")
            {
                BatteryLevel -= (int)(BatteryLevel * 0.05);
            }
            BatteryLevel -= (int)Math.Round(percent);
        }
        public void ChangeStatus()
        {
            if (IsDamaged == false)
            {
                IsDamaged = true;
                return;
            }
            IsDamaged = false;
        }

        public void Recharge()
        {
            BatteryLevel = 100;
        }
        public override string ToString()
        {
            string damaged = IsDamaged == true ? "damaged" : "OK";
            return
           $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: {damaged}";
        }
    }
}
