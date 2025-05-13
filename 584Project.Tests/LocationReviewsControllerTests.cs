using Xunit;
using Microsoft.EntityFrameworkCore;
using _584Project.Server.Controllers;
using dbModel;
using _584Project.Server.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;

public class LocationReviewsControllerTests
{
    private RestaurantContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<RestaurantContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var context = new RestaurantContext(options);

        // Ensure data only seeded once
        if (!context.Locations.Any())
        {
            context.Locations.Add(new Location
            {
                LocationId = 1,
                CityName = "Test City",
                Country = "Test Country",
                AdminName = "Admin",
                Latitude = 1.0f,
                Longitude = 1.0f,
                RestaurantName = "Test Restaurant"
            });

            context.Reviews.Add(new Review
            {
                ReviewId = 1,
                LocationId = 1,
                ReviewScore = 4.5f,
                ReviewCount = "100"
            });

            context.SaveChanges();
        }

        return context;
    }

    [Fact]
    public async Task GetAllLocationReviews_ReturnsData()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var controller = new LocationReviewsController(context);

        // Act
        var result = await controller.GetAllLocationReviews();

        // Assert
        var okResult = Assert.IsType<ActionResult<IEnumerable<LocationReviewDto>>>(result);
        var list = Assert.IsType<List<LocationReviewDto>>(okResult.Value);
        list.Should().HaveCount(1);
        list[0].CityName.Should().Be("Test City");
    }

    [Fact]
    public async Task GetLocationReviewById_ValidId_ReturnsReview()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var controller = new LocationReviewsController(context);

        // Act
        var result = await controller.GetLocationReviewById(1);

        // Assert
        var okResult = Assert.IsType<ActionResult<LocationReviewDto>>(result);
        var review = Assert.IsType<LocationReviewDto>(okResult.Value);
        review.RestaurantName.Should().Be("Test Restaurant");
    }

    [Fact]
    public async Task GetLocationReviewById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var controller = new LocationReviewsController(context);

        // Act
        var result = await controller.GetLocationReviewById(999);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
    }
}
