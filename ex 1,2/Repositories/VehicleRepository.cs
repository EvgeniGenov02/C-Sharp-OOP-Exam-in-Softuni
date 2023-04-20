using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDriveRent.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private readonly List<IVehicle> models;
        public VehicleRepository()
        {
            models = new List<IVehicle>();
        }
        public void AddModel(IVehicle model)
        {
            models.Add(model);
        }

        public IVehicle FindById(string identifier)
        {
            return models.FirstOrDefault(m => m.LicensePlateNumber == identifier);
        }

        public IReadOnlyCollection<IVehicle> GetAll()
        {
            return models;
        }

        public bool RemoveById(string identifier)
        {
            IVehicle user = FindById(identifier);
            if (user == null)
            {
                return false;
            }
            return models.Remove(user);
        }
    }
}
