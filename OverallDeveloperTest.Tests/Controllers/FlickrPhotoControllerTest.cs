using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OverallDeveloperTest;
using OverallDeveloperTest.Controllers;
using System.Web.Mvc;
using OverallDeveloperTest.Domain;
using Moq;
using System.Data.Entity;
using OverallDeveloperTest.Models;
using FlickrManager;
using FourSquareManager;
using System.Web;

namespace OverallDeveloperTest.Tests.Controllers
{
    [TestClass]
    public class FlickrPhotoControllerTest
    {
        private Mock<ILocationRepository> _mockLocationRepository;
        private Mock<IFlickrPhotoRepository> _mockFlickrPhotoRepository;
        private List<Location> _locations;
        private List<FlickrPhoto> _flickrPhoto;
        private readonly IFlickrService _FlickrService;
        private readonly IFourSquareService _FourSquareService;

        private Mock<ControllerContext> mockControllerContext;
        private Mock<HttpSessionStateBase> mockSession;
        public FlickrPhotoControllerTest()
        {

            _mockLocationRepository = new Mock<ILocationRepository>();
            _mockFlickrPhotoRepository = new Mock<IFlickrPhotoRepository>();
            _FlickrService = new FlickrService();
            _FourSquareService = new FourSquareService();
            _locations = new List<Location>

            {
                new Location { LocationID=  "123456", Name = "Cape Town"},
                new Location { LocationID=  "987654", Name = "Pretoria"},
                new Location { LocationID=  "101112", Name = "Durban"}
            };
            _flickrPhoto = new List<FlickrPhoto>();
            _mockLocationRepository.Setup(x => x.GetAll()).Returns(_locations);
            _mockLocationRepository.Setup(x => x.GetLocations(It.IsAny<string>())).Returns((string s ) => _locations.Where(x => x.LocationID == s).ToList());

            _mockFlickrPhotoRepository.Setup(x => x.GetAll()).Returns(_flickrPhoto);
            _mockFlickrPhotoRepository.Setup(x => x.GetByLocationId(It.IsAny<string>())).Returns((string s) => _flickrPhoto.Where(x => x.LocationID == s).ToList());
            _mockFlickrPhotoRepository.Setup(x => x.AddRange(It.IsAny<List<FlickrPhoto>>())).Callback((List<FlickrPhoto> f) => { _flickrPhoto.AddRange(f); } );

            mockControllerContext = new Mock<ControllerContext>();
            mockSession = new Mock<HttpSessionStateBase>();
            mockSession.SetupGet(s => s["LocationId"]).Returns("123"); //somevalue
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);


        }

        [TestMethod]
        public void GetLocations()
        {

            // Arrange
            FlickrPhotoController controller = new FlickrPhotoController(_mockLocationRepository.Object,_mockFlickrPhotoRepository.Object,_FourSquareService,_FlickrService);
            controller.ControllerContext = mockControllerContext.Object;

            // Act
            controller.GetLocations();
            var result = (SelectList)controller.ViewData["Locations"];
            
            // Assert
            Assert.AreEqual(3, result.Count());
        }
        [TestMethod]
        public void GetLocationCoordinate()
        {
            FlickrPhotoController controller = new FlickrPhotoController(_mockLocationRepository.Object, _mockFlickrPhotoRepository.Object, _FourSquareService, _FlickrService);
           controller.ControllerContext = mockControllerContext.Object;
            var result = controller.GetLocationCoordinate("123456");

            Assert.AreEqual(18.4173774719238, double.Parse(result.Longitude));
            Assert.AreEqual(-33.9281208675072, double.Parse(result.Latitude));

        }
        [TestMethod]
        public void GetLocationCoordinateInvalidID()
        {
            FlickrPhotoController controller = new FlickrPhotoController(_mockLocationRepository.Object, _mockFlickrPhotoRepository.Object, _FourSquareService, _FlickrService);
            controller.ControllerContext = mockControllerContext.Object;

            var result = controller.GetLocationCoordinate("99999");

            Assert.AreEqual(null,result);

        }

        [TestMethod]
        public void GetFlickrPhotoByCoordinate()
        {
            FlickrPhotoController controller = new FlickrPhotoController(_mockLocationRepository.Object, _mockFlickrPhotoRepository.Object, _FourSquareService, _FlickrService);
            controller.ControllerContext = mockControllerContext.Object;

            controller.GetFlickrPhotoByCoordinate(18.4173774719238, -33.9281208675072,"123456");

            var photo = _mockFlickrPhotoRepository.Object.GetByLocationId("123456");

            Assert.AreEqual(true, photo.Any());

        }

        [TestMethod]
        public void GetFlickrPhotoInvalidCoordinate()
        {
            FlickrPhotoController controller = new FlickrPhotoController(_mockLocationRepository.Object, _mockFlickrPhotoRepository.Object, _FourSquareService, _FlickrService);
            controller.ControllerContext = mockControllerContext.Object;

            try
            {
                controller.GetFlickrPhotoByCoordinate(181, 89, "123456");
                Assert.Fail("An Exception should have been thrown");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Not a valid longitude (999)", ex.Message);
            }
           

        }

        [TestMethod]
        public void GetFlickrPhoto()
        {

            // Arrange
            FlickrPhotoController controller = new FlickrPhotoController(_mockLocationRepository.Object, _mockFlickrPhotoRepository.Object, _FourSquareService, _FlickrService);
            controller.ControllerContext = mockControllerContext.Object;

            // Act
            var result = controller.GetFlickrPhoto(1,"123456",10);


            Assert.AreEqual(10, result.FlickrsPhotos.Count());
            Assert.AreEqual(1, result.FlickrsPhotos.PageNumber);
        }
        [TestMethod]
        public void GetFlickrPhotoInvalidPageNumber()
        {
            try
            {
                // Arrange
                FlickrPhotoController controller = new FlickrPhotoController(_mockLocationRepository.Object, _mockFlickrPhotoRepository.Object, _FourSquareService, _FlickrService);
                controller.ControllerContext = mockControllerContext.Object;

                // Act
                var result = controller.GetFlickrPhoto(0, "123456", 10);

                Assert.Fail("An Exception should have been thrown");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("PageNumber cannot be below 1.\r\nParameter name: pageNumber\r\nActual value was 0.", ex.Message);
            }


        }
        [TestMethod]
        public void GetFlickrPhotoInvalidPageNumber2()
        {
           
            // Arrange
            FlickrPhotoController controller = new FlickrPhotoController(_mockLocationRepository.Object, _mockFlickrPhotoRepository.Object, _FourSquareService, _FlickrService);
            controller.ControllerContext = mockControllerContext.Object;

            // Act
            var result = controller.GetFlickrPhoto(50, "123456", 10);

            Assert.AreEqual(0,result.FlickrsPhotos.Count());
        }

        [TestMethod]
        public void SearchFlickrPhoto()
        {
            FlickrPhotoController controller = new FlickrPhotoController(_mockLocationRepository.Object, _mockFlickrPhotoRepository.Object, _FourSquareService, _FlickrService);
            controller.ControllerContext = mockControllerContext.Object;

            // Act
            var result = controller.SearchFlickrPhoto(1, "123456", 10);

            Assert.AreEqual("FlickrPhotoPartial", result.ViewName);
            Assert.AreEqual(10, ((SearchInstagramPhotoViewModel)result.ViewData.Model).FlickrsPhotos.Count());


        }
    }
}
