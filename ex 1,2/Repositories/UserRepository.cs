using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDriveRent.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private readonly List<IUser> models;
        public UserRepository()
        {
            models = new List<IUser>();
        }
        public void AddModel(IUser model)
        {
            models.Add(model);
        }

        public IUser FindById(string identifier)
        {
            return models.FirstOrDefault(m => m.DrivingLicenseNumber == identifier);
        }

        public IReadOnlyCollection<IUser> GetAll()
        {
            return models;
        }

        public bool RemoveById(string identifier)
        {
            IUser user = FindById(identifier);
            if (user == null)
            {
                return false;
            }
            return models.Remove(user);
        }
    }
}
