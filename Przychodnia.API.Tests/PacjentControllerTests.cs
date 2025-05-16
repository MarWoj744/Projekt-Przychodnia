using IBLL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Models;
using Przychodnia.API.Controllers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class PacjentControllerTests
{
    private readonly Mock<IPacjentService> _mockService;
    private readonly PacjentController _controller;

    public PacjentControllerTests()
    {
        _mockService = new Mock<IPacjentService>();
        _controller = new PacjentController(_mockService.Object);
    }

    [Fact]
    public void GetAll_ReturnsOk_WithListOfPacjent()
    {
        var pacjenci = new List<Pacjent> {
            new Pacjent { Id = 1, PESEL = "12345678901" },
            new Pacjent { Id = 2, PESEL = "09876543210" }
        }.AsQueryable();

        _mockService.Setup(s => s.PobierzWszystkie()).Returns(pacjenci);

        var result = _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IQueryable<Pacjent>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenNotExists()
    {
        _mockService.Setup(s => s.GetPacjentById(It.IsAny<int>())).Returns((Pacjent)null);

        var result = _controller.GetById(99);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_ReturnsBadRequest_WhenModelInvalid()
    {
        _controller.ModelState.AddModelError("PESEL", "Required");

        var result = _controller.Create(new Pacjent());

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Create_ReturnsBadRequest_WhenPeselInvalid()
    {
        var pacjent = new Pacjent { PESEL = "123" };
        _mockService.Setup(s => s.ValidatePesel(pacjent)).Returns("Błędny PESEL");

        var result = _controller.Create(pacjent);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Błędny PESEL", badRequest.Value);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction_WhenValid()
    {
        var pacjent = new Pacjent { Id = 1, PESEL = "12345678901" };
        _mockService.Setup(s => s.ValidatePesel(pacjent)).Returns(string.Empty);

        var result = _controller.Create(pacjent);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(pacjent, createdResult.Value);
    }
}
