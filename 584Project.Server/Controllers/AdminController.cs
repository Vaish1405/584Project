using _584Project.Server.Dtos;
using dbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = _584Project.Server.Dtos.LoginRequest;

namespace _584Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<User> userManager, JwtHandler jwtHandler) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginRequest login)
        {
            User user = await userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                return Unauthorized("Unknown user");
            }
            bool success = await userManager.CheckPasswordAsync(user, login.Password);
            if (!success)
            {
                return Unauthorized("Wrong Password");
            }

            JwtSecurityToken token = await jwtHandler.GetTokenAsync(user);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginResponse
            {
                Success = true,
                Message = "works",
                Token = tokenString
            });
        }
    }
}
