﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dbModel;
using _584Project.Server.Dtos;

namespace _584Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public ReviewsController(RestaurantContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Reviews
        // Returns all reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews()
        {
            return await _context.Reviews
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    LocationId = r.LocationId,
                    ReviewScore = r.ReviewScore,
                    ReviewCount = r.ReviewCount
                })
                .ToListAsync();
        }

        // ✅ GET: api/Reviews/location/5
        // Returns all reviews for a specific location
        [HttpGet("location/{locationId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByLocationId(int locationId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.LocationId == locationId)
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    LocationId = r.LocationId,
                    ReviewScore = r.ReviewScore,
                    ReviewCount = r.ReviewCount
                })
                .ToListAsync();

            if (reviews == null || reviews.Count == 0)
            {
                return NotFound();
            }

            return reviews;
        }

        // ✅ PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.ReviewId)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // ✅ POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllReviews), new { id = review.ReviewId }, review);
        }

        // ✅ DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
