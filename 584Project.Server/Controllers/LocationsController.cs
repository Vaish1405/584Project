using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dbModel;  // Correct namespace for RestaurantContext
using _584Project.Server.Dtos;
using Microsoft.AspNetCore.Authorization;    // Correct namespace for LocationReviewDto

namespace _584Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public LocationsController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationReviewDto>>> GetLocations()
        {
            return await _context.Locations
                .Select(location => new LocationReviewDto
                {
                    LocationId = location.LocationId,
                    CityName = location.CityName,
                    Country = location.Country,
                    AdminName = location.AdminName,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    RestaurantName = location.RestaurantName,
                })
                .ToListAsync();
        }

        // GET: api/Locations/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationReviewDto>> GetLocation(int id)
        {
            var location = await _context.Locations
                .Where(l => l.LocationId == id)
                .Select(location => new LocationReviewDto
                {
                    LocationId = location.LocationId,
                    CityName = location.CityName,
                    Country = location.Country,
                    AdminName = location.AdminName,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    RestaurantName = location.RestaurantName,
                })
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            if (id != location.LocationId)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Locations
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.LocationId }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.LocationId == id);
        }
    }
}
