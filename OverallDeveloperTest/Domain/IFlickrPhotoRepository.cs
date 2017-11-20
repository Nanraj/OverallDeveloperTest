using OverallDeveloperTest.Models;
using System.Collections.Generic;

namespace OverallDeveloperTest.Domain
{
    public interface IFlickrPhotoRepository
    {
        List<FlickrPhoto> GetAll();
        List<FlickrPhoto> GetByLocationId(string locationId);
        void AddRange(List<FlickrPhoto> photos);
        int SaveChanges();
    }
}