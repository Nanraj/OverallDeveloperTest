using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using PagedList;

namespace OverallDeveloperTest.Models
{
    public class SearchInstagramPhotoViewModel
    {
        [Display(Name = "Location")]
        public string LocationId { get; set; }

        public IList<FlickrPhoto> Photos { get; set; }

        public PagedList.IPagedList<FlickrPhoto> FlickrsPhotos{get;set;}

        public Location Location { get; set; }

        public SearchInstagramPhotoViewModel()
        {
            FlickrsPhotos = new List<FlickrPhoto>().ToPagedList(1, 10); ;
            Location = new Location();
            Photos = new List<FlickrPhoto>();
        }
    }
}