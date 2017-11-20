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

namespace OverallDeveloperTest.Tests.Controllers
{
    [TestClass]
    public class LocationControllerTest
    {
        private Mock<IRepository> _mockRepository;
        private Mock<DbSet<Location>> _mockLocation;
        private Mock<DbSet<FlickrPhoto>> _mockFlickrPhoto;

        public LocationControllerTest()
        {
            _mockRepository = new Mock<IRepository>();
            _mockLocation = new Mock<DbSet<Location>>();
            _mockRepository.Setup(x => x.Locations).Returns(_mockLocation.Object);
            _mockRepository.Setup(x => x.FlickrPhotos).Returns(_mockFlickrPhoto.Object);
        }

        [TestMethod]
        public void AddLocation()
        {
            // Arrange
            LocationController controller = new LocationController(_mockRepository.Object);

            // Act
            var result = controller.AddLocation("Rodrigues"); 

            // Assert
            Assert.AreEqual(true, result.Data);
        }

        [TestMethod]
        public void AddLocationBlank()
        {
            LocationController controller = new LocationController(_mockRepository.Object);

            // Act
            var result = controller.AddLocation(null); ;

            // Assert
            Assert.AreEqual(false, result.Data);
        }
    }
}
