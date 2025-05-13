using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using _584Project.Server.Controllers;
using _584Project.Server.Dtos;
using dbModel;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Collections.Generic;

public class AdminControllerTests
{
    [Fact]
    public async Task LoginAsync_ValidUser_ReturnsToken()
    {
        // Arrange
        var userName = "testuser";
        var password = "testpassword";
        var user = new User { UserName = userName };

        var mockUserManager = MockUserManager();
        mockUserManager.Setup(um => um.FindByNameAsync(userName)).ReturnsAsync(user);
        mockUserManager.Setup(um => um.CheckPasswordAsync(user, password)).ReturnsAsync(true);

        var jwtHandlerMock = new Mock<_584Project.Server.JwtHandler>();
        jwtHandlerMock
            .Setup(j => j.GetTokenAsync(user))
            .ReturnsAsync(CreateFakeJwtToken());

        var controller = new AdminController(mockUserManager.Object, jwtHandlerMock.Object);

        var loginRequest = new LoginRequest
        {
            UserName = userName,
            Password = password
        };

        // Act
        var result = await controller.LoginAsync(loginRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<LoginResponse>(okResult.Value);
        Assert.True(response.Success);
        Assert.NotEmpty(response.Token);
    }

    // Utility: Create a fake JWT token
    private JwtSecurityToken CreateFakeJwtToken()
    {
        return new JwtSecurityToken(
            issuer: "test",
            audience: "test",
            claims: new List<Claim> { new Claim(ClaimTypes.Name, "testuser") },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: null // Not important for unit test
        );
    }

    // Utility: Create a mock UserManager
    private Mock<UserManager<User>> MockUserManager()
    {
        var store = new Mock<IUserStore<User>>();
        return new Mock<UserManager<User>>(
            store.Object, null, null, null, null, null, null, null, null);
    }
}
