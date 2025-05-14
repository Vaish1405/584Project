using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dbModel;
using _584Project.Server.Dtos;

namespace _584Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public StaffController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetAllStaff()
        {
            var staffList = await _context.Staff
                .Join(_context.Locations,
                      staff => staff.LocationId,
                      loc => loc.LocationId,
                      (staff, loc) => new StaffDto
                      {
                          StaffId = staff.StaffId,
                          FirstName = staff.FirstName,
                          LastName = staff.LastName,
                          Position = staff.Position,
                          LocationId = staff.LocationId,
                          RestaurantName = loc.RestaurantName,
                          CityName = loc.CityName,
                          Country = loc.Country
                      })
                .ToListAsync();

            return staffList;
        }

        // GET: api/Staff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetStaffById(int id)
        {
            var staff = await _context.Staff
                .Join(_context.Locations,
                      staff => staff.LocationId,
                      loc => loc.LocationId,
                      (staff, loc) => new StaffDto
                      {
                          StaffId = staff.StaffId,
                          FirstName = staff.FirstName,
                          LastName = staff.LastName,
                          Position = staff.Position,
                          LocationId = staff.LocationId,
                          RestaurantName = loc.RestaurantName,
                          CityName = loc.CityName,
                          Country = loc.Country
                      })
                .Where(s => s.StaffId == id)
                .FirstOrDefaultAsync();

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }

        // GET: api/Staff/ByLocation/5
        [HttpGet("ByLocation/{locationId}")]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetStaffByLocation(int locationId)
        {
            var staffList = await _context.Staff
                .Where(s => s.LocationId == locationId)
                .Join(_context.Locations,
                      staff => staff.LocationId,
                      loc => loc.LocationId,
                      (staff, loc) => new StaffDto
                      {
                          StaffId = staff.StaffId,
                          FirstName = staff.FirstName,
                          LastName = staff.LastName,
                          Position = staff.Position,
                          LocationId = staff.LocationId,
                          RestaurantName = loc.RestaurantName,
                          CityName = loc.CityName,
                          Country = loc.Country
                      })
                .ToListAsync();

            if (!staffList.Any())
                return NotFound();

            return staffList;
        }

        // POST: api/Staff
        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStaffById), new { id = staff.StaffId }, staff);
        }

        // PUT: api/Staff/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(int id, Staff staff)
        {
            if (id != staff.StaffId)
            {
                return BadRequest();
            }

            _context.Entry(staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
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

        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.StaffId == id);
        }
    }
}
