﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dbModel;
using Microsoft.AspNetCore.Identity;

namespace _584Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(RestaurantContext context, IHostEnvironment environment,
        UserManager<User> userManager) : ControllerBase
    {
        [HttpPost("Users")]
        public async Task ImportUsersAsync()
        {
            // user and admin passwords will be in notes
            User user = new()
            {
                UserName = "user",
                Email = "user@email.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            IdentityResult result = await userManager.CreateAsync(user, "Password0!");
            int resultSave = await context.SaveChangesAsync();
        }

        [HttpPost("Locations")]
        public async Task<ActionResult> ImportLocationsAsync()
        {
            // TODO: Add logic to import Locations
            return Ok("Locations import endpoint hit, but logic not yet implemented.");
        }

        [HttpPost("Reviews")]
        public async Task<ActionResult> ImportReviewsAsync()
        {
            // TODO: Add logic to import Reviews
            return Ok("Reviews import endpoint hit, but logic not yet implemented.");
        }
    }
}
