using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dbModel;
using _584Project.Server.Dtos;

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
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAllLocations()
        {
            return await _context.Locations
                .Select(loc => new LocationDto
                {
                    LocationId = loc.LocationId,
                    CityName = loc.CityName,
                    Country = loc.Country,
                    AdminName = loc.AdminName,
                    Latitude = loc.Latitude,
                    Longitude = loc.Longitude,
                    RestaurantName = loc.RestaurantName
                })
                .ToListAsync();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetLocationById(int id)
        {
            var location = await _context.Locations
                .Where(loc => loc.LocationId == id)
                .Select(loc => new LocationDto
                {
                    LocationId = loc.LocationId,
                    CityName = loc.CityName,
                    Country = loc.Country,
                    AdminName = loc.AdminName,
                    Latitude = loc.Latitude,
                    Longitude = loc.Longitude,
                    RestaurantName = loc.RestaurantName
                })
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // POST: api/Locations
        [HttpPost]
        public async Task<ActionResult<LocationDto>> PostLocation(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocationById), new { id = location.LocationId }, location);
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
