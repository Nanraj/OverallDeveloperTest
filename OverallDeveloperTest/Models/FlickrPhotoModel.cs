using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OverallDeveloperTest.Models
{
    public class FlickrPhoto
    {
        public string FlickrPhotoID { get; set; }

        public double PhotoID { get; set; }

        public string LocationID { get; set; }

        public string Description { get; set; }

        public string OwnerName { get; set; }

        public string Title { get; set; }

        public DateTime? DateUploaded { get; set; }

        public string OriginalURL { get; set; }

        public string LargeThumnailURL { get; set; }

        public virtual Location Location { get; set; }
    }
}