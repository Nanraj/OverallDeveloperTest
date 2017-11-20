using OverallDeveloperTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OverallDeveloperTest.Domain
{
    public interface IRepository
    {
        DbSet<Location> Locations { get;}
        DbSet<FlickrPhoto> FlickrPhotos { get;}
        int SaveChanges();
    }
}