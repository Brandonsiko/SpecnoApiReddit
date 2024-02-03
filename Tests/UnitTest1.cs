namespace Tests;

using System;
using Microsoft.AspNetCore.Mvc;
using SpecnoApiReddit.Controllers;
using SpecnoApiReddit.Models;
using Xunit;


public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var userServiceMock = new Mock<IUserService>();
        var controller = new UserController(userServiceMock.Object);

        // Act
        var result = controller.GetUserDetails(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}