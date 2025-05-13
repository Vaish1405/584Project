using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dbModel;
using _584Project.Server.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace _584Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationReviewsController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public LocationReviewsController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/LocationReviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationReviewDto>>> GetAllLocationReviews()
        {
            var locationReviews = await _context.Locations
                .Join(_context.Reviews,
                    loc => loc.LocationId,
                    rev => rev.LocationId,
                    (loc, rev) => new LocationReviewDto
                    {
                        LocationId = loc.LocationId,
                        CityName = loc.CityName,
                        Country = loc.Country,
                        AdminName = loc.AdminName,
                        Latitude = loc.Latitude,
                        Longitude = loc.Longitude,
                        RestaurantName = loc.RestaurantName,
                        ReviewScore = rev.ReviewScore,
                        ReviewCount = rev.ReviewCount
                    })
                .ToListAsync();

            return locationReviews;
        }

        // GET: api/LocationReviews/5
        [Authorize]
        [HttpGet("{locationId}")]
        public async Task<ActionResult<LocationReviewDto>> GetLocationReviewById(int locationId)
        {
            var locationReview = await _context.Locations
                .Join(_context.Reviews,
                    loc => loc.LocationId,
                    rev => rev.LocationId,
                    (loc, rev) => new LocationReviewDto
                    {
                        LocationId = loc.LocationId,
                        CityName = loc.CityName,
                        Country = loc.Country,
                        AdminName = loc.AdminName,
                        Latitude = loc.Latitude,
                        Longitude = loc.Longitude,
                        RestaurantName = loc.RestaurantName,
                        ReviewScore = rev.ReviewScore,
                        ReviewCount = rev.ReviewCount
                    })
                .Where(lr => lr.LocationId == locationId)
                .FirstOrDefaultAsync();

            if (locationReview == null)
            {
                return NotFound();
            }

            return locationReview;
        }
    }
}
