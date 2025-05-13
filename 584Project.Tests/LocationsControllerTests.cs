using Xunit;
using Microsoft.EntityFrameworkCore;
using _584Project.Server.Controllers;
using dbModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using _584Project.Server.Dtos;

public class LocationsControllerTests
{
    private RestaurantContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<RestaurantContext>()
            .UseInMemoryDatabase("Test_LocationsDb")
            .Options;

        var context = new RestaurantContext(options);

        if (!context.Locations.Any())
        {
            context.Locations.AddRange(
                new Location
                {
                    LocationId = 1,
                    CityName = "City A",
                    Country = "Country A",
                    AdminName = "Admin A",
                    Latitude = 10.0f,
                    Longitude = 20.0f,
                    RestaurantName = "Restaurant A"
                },
                new Location
                {
                    LocationId = 2,
                    CityName = "City B",
                    Country = "Country B",
                    AdminName = "Admin B",
                    Latitude = 30.0f,
                    Longitude = 40.0f,
                    RestaurantName = "Restaurant B"
                });
            context.SaveChanges();
        }

        return context;
    }

    [Fact]
    public async Task GetAllLocations_ReturnsAllLocations()
    {
        var context = GetInMemoryDbContext();
        var controller = new LocationsController(context);

        var result = await controller.GetAllLocations();

        var okResult = Assert.IsType<ActionResult<IEnumerable<LocationDto>>>(result);
        var list = Assert.IsType<List<LocationDto>>(okResult.Value);
        list.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetLocationById_ExistingId_ReturnsLocation()
    {
        var context = GetInMemoryDbContext();
        var controller = new LocationsController(context);

        var result = await controller.GetLocationById(1);

        var location = Assert.IsType<LocationDto>(result.Value);
        location.RestaurantName.Should().Be("Restaurant A");
    }

    [Fact]
    public async Task GetLocationById_NonExistentId_ReturnsNotFound()
    {
        var context = GetInMemoryDbContext();
        var controller = new LocationsController(context);

        var result = await controller.GetLocationById(999);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task PostLocation_CreatesNewLocation()
    {
        var context = GetInMemoryDbContext();
        var controller = new LocationsController(context);

        var newLocation = new Location
        {
            LocationId = 10,
            CityName = "City X",
            Country = "Country X",
            AdminName = "Admin X",
            Latitude = 1,
            Longitude = 2,
            RestaurantName = "Restaurant X"
        };

        var result = await controller.PostLocation(newLocation);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedLocation = Assert.IsType<Location>(createdResult.Value);
        returnedLocation.RestaurantName.Should().Be("Restaurant X");
    }

    [Fact]
    public async Task PutLocation_UpdatesExistingLocation()
    {
        var context = GetInMemoryDbContext();
        var controller = new LocationsController(context);

        var location = context.Locations.Find(1);
        location.RestaurantName = "Updated Name";

        var result = await controller.PutLocation(1, location);

        Assert.IsType<NoContentResult>(result);
        context.Locations.Find(1).RestaurantName.Should().Be("Updated Name");
    }

    [Fact]
    public async Task PutLocation_IdMismatch_ReturnsBadRequest()
    {
        var context = GetInMemoryDbContext();
        var controller = new LocationsController(context);

        var location = context.Locations.Find(1);
        var result = await controller.PutLocation(999, location);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteLocation_DeletesExistingLocation()
    {
        var context = GetInMemoryDbContext();
        var controller = new LocationsController(context);

        var result = await controller.DeleteLocation(2);

        Assert.IsType<NoContentResult>(result);
        context.Locations.Any(l => l.LocationId == 2).Should().BeFalse();
    }

    [Fact]
    public async Task DeleteLocation_NonExistentId_ReturnsNotFound()
    {
        var context = GetInMemoryDbContext();
        var controller = new LocationsController(context);

        var result = await controller.DeleteLocation(999);

        Assert.IsType<NotFoundResult>(result);
    }
}
