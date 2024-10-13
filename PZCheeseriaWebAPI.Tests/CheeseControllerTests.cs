using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PZCheeseriaWebAPI.Controllers;
using PZCheeseriaWebAPI.DTO;
using PZCheeseriaWebAPI.Helpers;
using PZCheeseriaWebAPI.Interfaces;

namespace PZCheeseriaWebAPI.Tests;

public class CheeseControllerTests
{
    private readonly Mock<ICheeseService> _mockService;
    private readonly CheeseController _controller;
    private readonly string internalServerError = "Internal Server Error";

    public CheeseControllerTests()
    {
        _mockService = new Mock<ICheeseService>();
        _controller = new CheeseController(_mockService.Object);
    }

    #region GET Unit tests

    [Fact]
    public void GetCheeseReturnsNotFound_WhenCheeseNotFound()
    {
        //arrange
        _mockService.Setup(s => s.GetCheese(9999)).Returns((CheeseDTO)null);

        // Act
        var result = _controller.GetCheese(9999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void GetCheeseReturnsOkResult_WithCheese()
    {
        //arrange
        var cheese = new CheeseDTO() { Id = 1, Name = "Test Cheese", PricePerKilo = 15, Color = "Yellow" };

        _mockService.Setup(s => s.GetCheese(1)).Returns((CheeseDTO)cheese);

        // Act
        var result = _controller.GetCheese(1);

        // Assert

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnCheese = Assert.IsType<CheeseDTO>(okResult.Value);
        Assert.Equal("Test Cheese", returnCheese.Name);
        Assert.Equal(15, returnCheese.PricePerKilo);
        Assert.Equal("Yellow", returnCheese.Color);
    }

    [Fact]
    public void GetAllReturnsOkResult_WithCheeseList()
    {
        // Arrange
        var cheeseList = new List<CheeseDTO> { new CheeseDTO { Id = 1, Name = "Cheddar" } };
        _mockService.Setup(service => service.GetCheeseList()).Returns(cheeseList);

        // Act
        var result = _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnCheeseList = Assert.IsType<List<CheeseDTO>>(okResult.Value);
        Assert.Single(returnCheeseList);
    }

    [Fact]
    public void GetAll_ReturnsInternalServerError_WhenExceptionIsThrown()
    {
        // Arrange
        _mockService.Setup(service => service.GetCheeseList()).Throws(new ExceptionHelper(internalServerError));

        // Act
        var result = _controller.GetAll();

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal(internalServerError, statusCodeResult.Value);
    }

    [Fact]
    public void GetCheese_ReturnsInternalServerError_WhenExceptionIsThrown()
    {
        // Arrange
        int cheeseId = 1;
        _mockService.Setup(service => service.GetCheese(cheeseId)).Throws(new ExceptionHelper(internalServerError));

        // Act
        var result = _controller.GetCheese(cheeseId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal(internalServerError, statusCodeResult.Value);
    }

    #endregion GET Unit tests

    #region Post Unit tests
    [Fact]
    public void PostReturns_CreatedAtActionResult()
    {
        // Arrange
        var cheese = new CheeseDTO { Name = "Cheddar" };
        _mockService.Setup(service => service.AddCheeseToTable(cheese)).Returns(cheese);

        // Act
        var result = _controller.Post(cheese);

        // Assert
        var cheeseCreatedAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnCheese = Assert.IsType<CheeseDTO>(cheeseCreatedAtActionResult.Value);
        Assert.Equal("Cheddar", returnCheese.Name);
    }
    [Fact]
    public void Create_ReturnsInternalServerError_WhenExceptionIsThrown()
    {
        // Arrange
        var cheese = new CheeseDTO { Name = "American Cheese", Color ="Yellow"  };
        _mockService.Setup(service => service.AddCheeseToTable(cheese)).Throws(new ExceptionHelper(internalServerError));

        // Act
        var result = _controller.Post(cheese);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal(internalServerError, statusCodeResult.Value);
    }
    #endregion

    #region PUT
    [Fact]
    public void Put_ReturnsOkResult_WithUpdatedCheese()
    {
        // Arrange
        var cheese = new CheeseDTO { Id = 1, Name = "Brie" };
        _mockService.Setup(service => service.UpdateCheese(1, cheese)).Returns(cheese);

        // Act
        var result = _controller.Put(1, cheese);

        // Assert
        var okPutResult = Assert.IsType<OkObjectResult>(result);
        var returnCheese = Assert.IsType<CheeseDTO>(okPutResult.Value);
        Assert.Equal("Brie", returnCheese.Name);
    }

    [Fact]
    public void Put_ReturnsNotFound()
    {
        // Arrange
        var cheese = new CheeseDTO { Name = "Baby bel", Color = "Pale Yellow" };
        _mockService.Setup(service => service.UpdateCheese(999, cheese)).Returns((CheeseDTO)null);

        // Act
        var result = _controller.Put(999, cheese);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Update_ReturnsInternalServerError_WhenExceptionIsThrown()
    {
        // Arrange
        var cheeseId = 1; //Existing cheese id
        var cheese = new CheeseDTO { Id = cheeseId, Name = "American Cheese", Color = "Yellow" };
        _mockService.Setup(service => service.UpdateCheese( cheeseId, cheese)).Throws(new ExceptionHelper(internalServerError));

        // Act
        var result = _controller.Put(cheeseId ,cheese);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal(internalServerError, statusCodeResult.Value);
    }
    #endregion
    #region DELETE
    [Fact]
    public void DeletedCheese_IsNotFound()
    {
        // Arrange
        int cheeseId = 9999; // Non-existent cheese id
        _mockService.Setup(service => service.DeleteCheese(cheeseId)).Returns(false);

        // Act
        var deleteResult = _controller.Delete(cheeseId);

        // Assert
        Assert.IsType<NotFoundResult>(deleteResult);
    }

    [Fact]
    public void DeleteCheese_ReturnsOkResult()
    {
        // Arrange
        int cheeseId = 1; // Non-existent cheese id
        _mockService.Setup(service => service.DeleteCheese(cheeseId)).Returns(true);

        // Act
        var deleteResult = _controller.Delete(cheeseId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(deleteResult);
        Assert.True((bool)okResult.Value);
    }

    [Fact]
    public void Delete_ReturnsInternalServerError_WhenExceptionIsThrown()
    {
        // Arrange
        int cheeseId = 1;
        _mockService.Setup(service => service.DeleteCheese(cheeseId)).Throws(new ExceptionHelper(internalServerError));

        // Act
        var result = _controller.Delete(cheeseId);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        Assert.Equal(internalServerError, statusCodeResult.Value);
    }
    #endregion
}