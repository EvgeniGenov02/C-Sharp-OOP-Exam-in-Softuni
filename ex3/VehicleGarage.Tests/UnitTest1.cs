using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using NUnit.Framework;

namespace VehicleGarage.Tests
{
    public class Tests
    {

        // test Constructor
        [Test]
        public void TestGarageCapacity()
        {
            Garage garage = new Garage(1);
            Assert.AreEqual(1, garage.Capacity);
        }

        // test AddVehicle()
        [Test]
        public void TestGarageAddVehicleReturnTrue()
        {
            Garage garage = new Garage(1);
            Vehicle vehicle = new Vehicle("Brand", "BMW", "XAE12", 1000);
            Assert.IsTrue(garage.AddVehicle(vehicle));
            Assert.AreEqual(garage.Vehicles.Count, 1);

        }

        [Test]
        public void TestGarageAddVehicleReturnFalse()
        {
            Garage garage = new Garage(0);
            Vehicle vehicle = new Vehicle("Brand", "BMW", "XAE12", 1000);
            Assert.IsFalse(garage.AddVehicle(vehicle));
            Assert.AreEqual(garage.Vehicles.Count, 0);
        }
       

        //test ChargeVehicles()
        [Test]
        public void ChargeVehiclesTest()
        {
            Garage garage = new Garage(2);

            Vehicle vehicle1 = new Vehicle("Brand", "BMW", "XAE12", 1000);
            Vehicle vehicle2 = new Vehicle("toyota", "supra", "TS2023", 300);

            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            Assert.AreEqual(garage.ChargeVehicles(100), 2);
        }
        [Test]
        public void ChargeVehiclesTest1()
        {
            Garage garage = new Garage(2);

            Vehicle vehicle1 = new Vehicle("Brand", "BMW", "XAE12", 1000);
            Vehicle vehicle2 = new Vehicle("toyota", "supra", "TS2023", 300);

            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            Assert.AreEqual(garage.ChargeVehicles(10), 0);
        }
        //test DriveVehicle()
        [Test]
    
        public void DriveVehicleTestDrop()
        {
            Garage garage = new Garage(2);

            Vehicle vehicle = new Vehicle("toyota", "supra", "TS2023", 300);
       
            garage.AddVehicle(vehicle);

            garage.DriveVehicle("TS2023", 10, true);
            Assert.AreEqual(vehicle.BatteryLevel,90);
            Assert.IsTrue(vehicle.IsDamaged);
        }

        [Test]

        public void DriveVehicleTest()
        {
            Garage garage = new Garage(2);

            Vehicle vehicle = new Vehicle("toyota", "supra", "TS2023", 300);

            garage.AddVehicle(vehicle);

            garage.DriveVehicle("TS2023", 1000, false);
            Assert.AreEqual(vehicle.BatteryLevel, 100);
            Assert.IsFalse(vehicle.IsDamaged);
        }
        [Test]

        public void DriveVehicleTest0()
        {
            Garage garage = new Garage(2);

            Vehicle vehicle = new Vehicle("toyota", "supra", "TS2023", 300);

            garage.AddVehicle(vehicle);

            garage.DriveVehicle("TS2023", -33, true);
            Assert.AreEqual(vehicle.BatteryLevel, 133);
            Assert.IsTrue(vehicle.IsDamaged);
        }
        // test RepairVehicles()
        [Test]

        public void RepairVehicles()
        {
            Garage garage = new Garage(2);

            Vehicle vehicle = new Vehicle("toyota", "supra", "TS2023", 300);

            garage.AddVehicle(vehicle);
            garage.DriveVehicle("TS2023", 10, true);

            Assert.AreEqual(vehicle.BatteryLevel, 90);
            Assert.IsTrue(vehicle.IsDamaged);

            Assert.AreEqual(garage.RepairVehicles(), "Vehicles repaired: 1");
        }

        [Test]

        public void RepairVehicles0()
        {
            Garage garage = new Garage(2);

            Vehicle vehicle = new Vehicle("toyota", "supra", "TS2023", 300);

            garage.AddVehicle(vehicle);
            garage.DriveVehicle("TS2023", 10, false);

            Assert.AreEqual(vehicle.BatteryLevel, 90);
            Assert.IsFalse(vehicle.IsDamaged);

            Assert.AreEqual(garage.RepairVehicles(), "Vehicles repaired: 0");
        }
    }
}