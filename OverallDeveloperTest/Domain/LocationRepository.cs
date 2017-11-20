
using OverallDeveloperTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverallDeveloperTest.Domain
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IRepository _Repository;
        public LocationRepository(IRepository repository)
        {
            _Repository = repository;
        }

        public List<Location> GetAll()
        {
            return _Repository.Locations.OrderBy(x => x.Name).ToList();
        }

        public List<Location> SearchByName(string name)
        {
            return _Repository.Locations.Where(x => x.Name == name).ToList();
        }

        public List<Location> GetLocations(string locationId)
        {
            return _Repository.Locations.Where(x => x.LocationID == locationId).ToList();
        }

        public int SaveChanges()
        {
            return _Repository.SaveChanges();
        }
    }
}