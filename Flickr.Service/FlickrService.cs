using System.Configuration;
using FlickrNet;

namespace FlickrManager
{
    public class FlickrService : IFlickrService
    {
        private Flickr _FlickrInstance;

        public static string _ApiKey = ConfigurationManager.AppSettings["flickr_key"].ToString();
        public static string _SharedSecret = ConfigurationManager.AppSettings["flickr_secret"].ToString();

        public FlickrService()
        {
            Flickr.CacheDisabled = true;
            _FlickrInstance = new Flickr(_ApiKey, _SharedSecret);
        }

        public PhotoCollection SearchPhotos(double longitude, double latitude)
        {
            var options = GetSearchOption(longitude, latitude);

            PhotoCollection photos = _FlickrInstance.PhotosSearch(options);

            return photos;
        }

        private PhotoSearchOptions GetSearchOption(double longitude, double latitude)
        {
            PhotoSearchOptions options = new PhotoSearchOptions
            {
                Extras = PhotoSearchExtras.AllUrls | PhotoSearchExtras.Description | PhotoSearchExtras.OwnerName,
                SortOrder = PhotoSearchSortOrder.Relevance,
                Longitude = longitude,
                Latitude = latitude
            };

            return options;
        }
    }
}
