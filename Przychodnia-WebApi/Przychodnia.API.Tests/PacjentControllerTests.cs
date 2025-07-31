using IBLL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Models;
using Przychodnia.API.Controllers;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Models.Mapper;
using DTOs;

public class PacjentControllerTests
{
    private readonly Mock<IPacjentService> _mockService;
    private readonly PacjentController _controller;
    private readonly Mapper map;

    public PacjentControllerTests()
    {
        _mockService = new Mock<IPacjentService>();
        _controller = new PacjentController(_mockService.Object);
        map = new Mapper();
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

        var result = _controller.Create(new PacjentDTO());

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Create_ReturnsBadRequest_WhenPeselInvalid()
    {
        var pacjent = new Pacjent { PESEL = "123" };
        _mockService.Setup(s => s.ValidatePesel(It.IsAny<Pacjent>())).Returns("Błędny PESEL");

        PacjentDTO pacjentDTO = map.PacjentToDTO(pacjent);
        var result = _controller.Create(pacjentDTO);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Błędny PESEL", badRequest.Value);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction_WhenValid()
    {
        var pacjent = new Pacjent { Id = 1, PESEL = "12345678901" };
        _mockService.Setup(s => s.ValidatePesel(pacjent)).Returns(string.Empty);

        PacjentDTO pacjentDTO = map.PacjentToDTO(pacjent);

        var result = _controller.Create(pacjentDTO);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdPacjent = Assert.IsType<Pacjent>(createdResult.Value);
        Assert.Equal(pacjentDTO.Id, createdPacjent.Id);
        Assert.Equal(pacjentDTO.PESEL, createdPacjent.PESEL);
    }
}
