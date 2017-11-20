using OverallDeveloperTest.Domain;
using OverallDeveloperTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OverallDeveloperTest.Controllers
{
    public class LocationController : Controller
    {

        private readonly IRepository _Repository;
        public LocationController(IRepository repository)
        {
            _Repository = repository;
        }
        // GET: Location
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddLocation(string name)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(name))
            {
                Location location = new Location();
                location.LocationID = Guid.NewGuid().ToString();
                location.Name = name;

                _Repository.Locations.Add(location);

                _Repository.SaveChanges();

                return Json(true);
            }
            return Json(false);
        }
    }
}