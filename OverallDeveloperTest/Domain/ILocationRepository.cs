using OverallDeveloperTest.Models;
using System.Collections.Generic;

namespace OverallDeveloperTest.Domain
{
    public interface ILocationRepository
    {
        List<Location> GetAll();
        List<Location> SearchByName(string name);
        List<Location> GetLocations(string locationId);
        int SaveChanges();
    }
}