using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDriveRent.Repositories
{
    public class RouteRepository : IRepository<IRoute>
    {
        private readonly List<IRoute> models;
        public RouteRepository()
        {
            models = new List<IRoute>();
        }
        public void AddModel(IRoute model)
        {
            models.Add(model);
        }

        public IRoute FindById(string identifier)
        {

            return models.FirstOrDefault(m => m.RouteId == int.Parse(identifier));
        }

        public IReadOnlyCollection<IRoute> GetAll()
        {
            return models;
        }

        public bool RemoveById(string identifier)
        {
            IRoute user = FindById(identifier);
            if (user == null)
            {
                return false;
            }
            return models.Remove(user);
        }
    }
}
