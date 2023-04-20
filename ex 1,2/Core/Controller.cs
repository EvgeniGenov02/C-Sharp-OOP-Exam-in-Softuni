using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        //•	users – UserRepository
        //•	vehicles – VehicleRepository
        //•	routes – RouteRepository
        UserRepository users;
        VehicleRepository vehicles;
        RouteRepository routes;
        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository(); 
            routes = new RouteRepository();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            if (users.FindById(drivingLicenseNumber) != null)
            {
                return String.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }
            IUser user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);
            return String.Format
             (OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
        }
        public string UploadVehicle(string vehicleType, string brand, string model, 
         string licensePlateNumber)
        {
            IVehicle vehicle = null;
            if (vehicleType == "PassengerCar")
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            else if (vehicleType == "CargoVan")
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }
            else
            {
                return String.Format
            (OutputMessages.VehicleTypeNotAccessible, vehicleType);
            }
            if (vehicles.FindById(licensePlateNumber) != null)
            {
                return String.Format
            (OutputMessages.LicensePlateExists, licensePlateNumber);
            }
            vehicles.AddModel(vehicle);
            return String.Format
            (OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
        }
        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            int routeId = routes.GetAll().Count + 1;
            IRoute route = null;
            foreach (var routes in routes.GetAll())
            {
                if (routes.StartPoint == startPoint
                   && routes.EndPoint == endPoint
                   && routes.Length == length)
                {
                    return 
                    String.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
                }
                if (routes.StartPoint == startPoint
                   && routes.EndPoint == endPoint
                   && routes.Length < length)
                {
                    return
                    String.Format(OutputMessages.RouteIsTooLong,
                    startPoint, endPoint);
                }
                if (routes.StartPoint == startPoint
                   && routes.EndPoint == endPoint
                   && routes.Length > length)
                {
                    routes.LockRoute();
                }
            }
            route = new Route(startPoint, endPoint, length, routeId);
            routes.AddModel(route);
            return
            String.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
        }
        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber,
         string routeId, bool isAccidentHappened)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            IVehicle vehicle = vehicles.FindById(licensePlateNumber);
            IRoute route = routes.FindById(routeId);
            if (user.IsBlocked == true)
            {
                return String.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }
            if (vehicle.IsDamaged == true)
            {
                return String.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }
            if (route.IsLocked == true)
            {
                return String.Format(OutputMessages.RouteLocked, routeId);
            }

            vehicles.FindById(licensePlateNumber).Drive(route.Length);
            if (isAccidentHappened == true)
            {
                if (vehicles.FindById(licensePlateNumber).IsDamaged == false)
                {
                vehicles.FindById(licensePlateNumber).ChangeStatus();
                }
                users.FindById(drivingLicenseNumber).DecreaseRating();
            }
            else
            {
                users.FindById(drivingLicenseNumber).IncreaseRating();
            }

            return vehicles.FindById(licensePlateNumber).ToString();

        }

        public string RepairVehicles(int count)
        {
            int countOfRepairedVehicles = 0;
            List<IVehicle> vehiclesDamaged = vehicles.GetAll().Where(m=> m.IsDamaged == true)
              .OrderBy(m=>m.Brand).ThenBy(m=>m.Model).Take(count)
              .ToList();
            foreach (var vehicle in vehiclesDamaged)
            {
                vehicles.FindById(vehicle.LicensePlateNumber).Recharge();
                vehicles.FindById(vehicle.LicensePlateNumber).ChangeStatus();
                countOfRepairedVehicles++;
            }
            return String.Format(OutputMessages.RepairedVehicles, countOfRepairedVehicles);
        }

        public string UsersReport()
        {
            StringBuilder stringBuilder= new StringBuilder();
            List<IUser> user = users.GetAll().OrderByDescending(m=>m.Rating)
                .ThenBy(m=>m.LastName)
                .ThenBy(m=> m.FirstName)
                .ToList() ;
            stringBuilder.AppendLine("*** E-Drive-Rent ***");
            foreach (var item in user)
            {
                stringBuilder.AppendLine(item.ToString());
            }
            return stringBuilder.ToString().Trim();
        }
    }
}
