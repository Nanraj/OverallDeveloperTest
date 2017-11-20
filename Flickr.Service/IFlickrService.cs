using FlickrNet;

namespace FlickrManager
{
    public interface IFlickrService
    {
        PhotoCollection SearchPhotos(double longitude, double latitude);
    }
}
