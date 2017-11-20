using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OverallDeveloperTest.Models;
using OverallDeveloperTest.Domain;

namespace OverallDeveloperTest.DAL
{
    public class LocationInitializer: System.Data.Entity.CreateDatabaseIfNotExists<Repository>
    {
        protected override void Seed(Repository repository)
        {
            var Locations = new List<Location>
            {
                new Location {LocationID = Guid.NewGuid().ToString(), Name="Cape Town" ,Longitude="",Latitude=""},
                new Location {LocationID = Guid.NewGuid().ToString(), Name="Durban" ,Longitude="",Latitude=""},
                new Location {LocationID = Guid.NewGuid().ToString(), Name="Pretoria" ,Longitude="",Latitude=""}
            };

            Locations.ForEach(s => repository.Locations.Add(s));
            repository.SaveChanges();
            base.Seed(repository);
        }
       
    }
}