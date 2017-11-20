
using OverallDeveloperTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace OverallDeveloperTest.Domain
{
    public class FlickrPhotoRepository : IFlickrPhotoRepository
    {
        private readonly IRepository _Repository;
        public FlickrPhotoRepository(IRepository repository)
        {
            _Repository = repository;
        }

        public List<FlickrPhoto> GetAll()
        {
            return _Repository.FlickrPhotos.OrderBy(x => x.PhotoID).ToList();
        }

        public List<FlickrPhoto> GetByLocationId(string locationId)
        {
            return _Repository.FlickrPhotos.Where(x => x.LocationID == locationId).OrderBy(l => l.PhotoID).ToList();
        }

        public int SaveChanges()
        {
            return _Repository.SaveChanges();
        }

        public void AddRange(List<FlickrPhoto> flickrPhotos)
        {
            _Repository.FlickrPhotos.AddRange(flickrPhotos);
            _Repository.SaveChanges();
        }
    }
}