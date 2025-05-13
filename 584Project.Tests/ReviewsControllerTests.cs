using Xunit;
using Microsoft.EntityFrameworkCore;
using _584Project.Server.Controllers;
using dbModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using _584Project.Server.Dtos;
using System.Linq;

public class ReviewsControllerTests
{
    private RestaurantContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<RestaurantContext>()
            .UseInMemoryDatabase(databaseName: "Test_ReviewsDb")
            .Options;

        var context = new RestaurantContext(options);

        if (!context.Reviews.Any())
        {
            context.Reviews.AddRange(
                new Review
                {
                    ReviewId = 1,
                    LocationId = 100,
                    ReviewScore = 4.5f,
                    ReviewCount = "20"
                },
                new Review
                {
                    ReviewId = 2,
                    LocationId = 200,
                    ReviewScore = 3.0f,
                    ReviewCount = "5"
                });

            context.SaveChanges();
        }

        return context;
    }

    [Fact]
    public async Task GetAllReviews_ReturnsAll()
    {
        var context = GetInMemoryDbContext();
        var controller = new ReviewsController(context);

        var result = await controller.GetAllReviews();

        var okResult = Assert.IsType<ActionResult<IEnumerable<ReviewDto>>>(result);
        var list = Assert.IsType<List<ReviewDto>>(okResult.Value);
        list.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetReviewsByLocationId_ValidId_ReturnsReviews()
    {
        var context = GetInMemoryDbContext();
        var controller = new ReviewsController(context);

        var result = await controller.GetReviewsByLocationId(100);

        var okResult = Assert.IsType<ActionResult<IEnumerable<ReviewDto>>>(result);
        var list = Assert.IsType<List<ReviewDto>>(okResult.Value);
        list.Should().ContainSingle();
        list[0].ReviewScore.Should().Be(4.5f);
    }

    [Fact]
    public async Task GetReviewsByLocationId_InvalidId_ReturnsNotFound()
    {
        var context = GetInMemoryDbContext();
        var controller = new ReviewsController(context);

        var result = await controller.GetReviewsByLocationId(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostReview_AddsReview()
    {
        var context = GetInMemoryDbContext();
        var controller = new ReviewsController(context);

        var newReview = new Review
        {
            ReviewId = 10,
            LocationId = 300,
            ReviewScore = 5.0f,
            ReviewCount = "50"
        };

        var result = await controller.PostReview(newReview);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var review = Assert.IsType<Review>(created.Value);
        review.ReviewScore.Should().Be(5.0f);
    }

    [Fact]
    public async Task PutReview_ValidId_UpdatesReview()
    {
        var context = GetInMemoryDbContext();
        var controller = new ReviewsController(context);

        var review = context.Reviews.Find(1);
        review.ReviewScore = 2.2f;

        var result = await controller.PutReview(1, review);

        Assert.IsType<NoContentResult>(result);
        context.Reviews.Find(1).ReviewScore.Should().Be(2.2f);
    }

    [Fact]
    public async Task PutReview_IdMismatch_ReturnsBadRequest()
    {
        var context = GetInMemoryDbContext();
        var controller = new ReviewsController(context);

        var review = context.Reviews.Find(1);
        var result = await controller.PutReview(999, review);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteReview_ValidId_RemovesReview()
    {
        var context = GetInMemoryDbContext();
        var controller = new ReviewsController(context);

        var result = await controller.DeleteReview(2);

        Assert.IsType<NoContentResult>(result);
        context.Reviews.Any(r => r.ReviewId == 2).Should().BeFalse();
    }

    [Fact]
    public async Task DeleteReview_InvalidId_ReturnsNotFound()
    {
        var context = GetInMemoryDbContext();
        var controller = new ReviewsController(context);

        var result = await controller.DeleteReview(999);

        Assert.IsType<NotFoundResult>(result);
    }
}
