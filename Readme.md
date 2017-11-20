# Overall Developer Assessment - FourSquare/Fickr Implementation

*  Using SharpSquare Library to call Foursquare API to retrieve geolocation
*  Using FlickrNet Library to retrieve images from Flickr API based on geolocation
*  Pagination capability
*  Saving images and description in the database
*  Using Windsor Castle for DI
*  Unit test Implementation

## Getting Started

### Installing

1. Modify the *connections.config* connectionstring accordingly
2. Create IIS Virtual Directory *local.flickr.web* and point to OverallDeveloperTest\OverallDeveloperTest folder.
3. Edit host file, add new record 
```
127.0.0.1  local.flickr.web 
```

### Using the system
1. Register on the app
2. From menu click on search photo
3. Select Location and Click Search
4. Click "Add New" to add new location

## Running the tests

All functionality can also be tested via OverallDeveloperTest.Tests project using Visual Studio Test Explorer


## Author

* **Nanraj Doomun** 



