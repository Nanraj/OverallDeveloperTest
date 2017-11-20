using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OverallDeveloperTest.Models
{
    public class Location
    {
        public string LocationID { get; set; }
        [Required]
        [Display(Name="Location")]
        public string Name { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public virtual ICollection<FlickrPhoto> FlickrPhotos { get; set; }
    }
}