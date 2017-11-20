using Microsoft.AspNet.Identity.EntityFramework;
using OverallDeveloperTest.Models;
using System.Data.Entity;

namespace OverallDeveloperTest.Domain
{
    public class Repository : IdentityDbContext<ApplicationUser>, IRepository
    {
        public Repository(): base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Location> Locations { get; set; }
        public DbSet<FlickrPhoto> FlickrPhotos { get; set; }

        public static Repository Create()
        {
            return new Repository();
        }

    }
} 