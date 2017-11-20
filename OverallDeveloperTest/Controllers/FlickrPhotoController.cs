using FlickrManager;
using FlickrNet;
using FourSquareManager;
using OverallDeveloperTest.Domain;
using OverallDeveloperTest.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web.Mvc;

namespace OverallDeveloperTest.Controllers
{
    public class FlickrPhotoController : Controller
    {
        private readonly ILocationRepository _LocationRepository;
        private readonly IFlickrPhotoRepository _FlickrPhotoRepository;
        private readonly IFlickrService _FlickrService;
        private readonly IFourSquareService _FourSquareService;
        public FlickrPhotoController(ILocationRepository locationRepository, IFlickrPhotoRepository flickrPhotoRepository, 
            IFourSquareService fourSquareService, IFlickrService flickrService)
        {
            _LocationRepository = locationRepository;
            _FlickrPhotoRepository = flickrPhotoRepository;
            _FourSquareService = fourSquareService;
            _FlickrService = flickrService;
        }

        public ActionResult Index()
        {
            SearchInstagramPhotoViewModel viewmodel = new SearchInstagramPhotoViewModel();
            GetLocations();
            return View(viewmodel);
        }

        public PartialViewResult SearchFlickrPhoto(int page=1, string locationId = "locationId",int pageSize = 10)
        {
            if (locationId != "locationId")
                Session["LocationId"] = locationId;
            else
                locationId = Session["LocationId"].ToString();

            return PartialView("FlickrPhotoPartial" , GetFlickrPhoto(page, locationId, pageSize));
        }

        public SearchInstagramPhotoViewModel GetFlickrPhoto(int page=1, string locationId = "locationId", int pageSize = 10)
        {
            SearchInstagramPhotoViewModel viewmodel = new SearchInstagramPhotoViewModel();
            SearchFlickrAndUpdateDB(locationId);
            viewmodel.FlickrsPhotos = _FlickrPhotoRepository.GetByLocationId(locationId).ToPagedList(page, pageSize);
            return viewmodel;
        }



        #region Methods
        public void GetLocations()
        {
            Session["LocationId"] = string.Empty;
            var items = new List<SelectListItem>();
            var locations = _LocationRepository.GetAll();
            
            foreach (var dto in locations)
            {
                items.Add(new SelectListItem { Value = dto.LocationID, Text = dto.Name });
            }
            var list = new SelectList(items, "Value", "Text");
            ViewBag.Locations = list;
        }
        public Location GetLocationCoordinate(string locationId)
        {
            var locationList = _LocationRepository.GetLocations(locationId);
            if (locationList != null)
            {
                var location = locationList.FirstOrDefault();
                if (string.IsNullOrEmpty(location.Latitude) || string.IsNullOrEmpty(location.Latitude))
                {
                    var venue = _FourSquareService.SearchVenues(location.Name);
                    if (venue.Any())
                    {
                        location.Longitude = venue.FirstOrDefault().location.lng.ToString();
                        location.Latitude = venue.FirstOrDefault().location.lat.ToString();
                        _LocationRepository.SaveChanges();
                    }
                }
                return location;
            }
            return null;
        }
        public void GetFlickrPhotoByCoordinate(string lng, string lat, string locationId)
        {
            PhotoCollection photos = _FlickrService.SearchPhotos(double.Parse(lng), double.Parse(lat));

            var photoList = photos.OrderBy(x => x.PhotoId).ToList();

            var photoByLocationList = _FlickrPhotoRepository.GetByLocationId(locationId);

            if (photoByLocationList.Any())
            {
                var maxPhotoId = photoByLocationList.Max(x => x.PhotoID);

                photoList = photoList.Where(x => double.Parse(x.PhotoId) > maxPhotoId).ToList();

            }

            List<FlickrPhoto> flickrPhotos = new List<FlickrPhoto>();
            foreach (var flickrPhoto in photoList)
            {
                FlickrPhoto photo = new FlickrPhoto
                {
                    FlickrPhotoID = Guid.NewGuid().ToString(),
                    LocationID = locationId,
                    DateUploaded = (flickrPhoto.DateUploaded >= (DateTime)SqlDateTime.MinValue) ? flickrPhoto.DateUploaded : (DateTime?)null,
                    Description = flickrPhoto.Description,
                    PhotoID = double.Parse(flickrPhoto.PhotoId),
                    OwnerName = flickrPhoto.OwnerName,
                    Title = flickrPhoto.Title,
                    OriginalURL = String.IsNullOrEmpty(flickrPhoto.OriginalUrl) ? flickrPhoto.LargeSquareThumbnailUrl : flickrPhoto.OriginalUrl,
                    LargeThumnailURL = flickrPhoto.LargeSquareThumbnailUrl
                };

                flickrPhotos.Add(photo);
            }

            _FlickrPhotoRepository.AddRange(flickrPhotos);
        }
        public void SearchFlickrAndUpdateDB(string locationId)
        {
            //Retrieve Location coordinates
            var location = GetLocationCoordinate(locationId);

            //Get Photos from Flickr
            GetFlickrPhotoByCoordinate(location.Longitude, location.Latitude, locationId);
        }
        #endregion
    }
}