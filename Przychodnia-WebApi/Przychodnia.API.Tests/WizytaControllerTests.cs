using IBLL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Models;
using DTOs;
using Przychodnia.API.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Models.Mapper;

public class WizytaControllerTests
{
    private readonly Mock<IWizytaService> _mockService;
    private readonly WizytaController _controller;
    private readonly Mapper map;

    public WizytaControllerTests()
    {
        _mockService = new Mock<IWizytaService>();
        _controller = new WizytaController(_mockService.Object);
        map = new Mapper();
    }

    [Fact]
    public void GetAll_ReturnsOk_WithList()
    {
        var list = new List<Wizyta>
        {
            new Wizyta { Id = 1 },
            new Wizyta { Id = 2 }
        }.AsQueryable();

        _mockService.Setup(s => s.GetAll()).Returns(list);

        var result = _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IQueryable<Wizyta>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenNull()
    {
        _mockService.Setup(s => s.GetWizytaById(It.IsAny<int>())).Returns((Wizyta)null);

        var result = _controller.GetById(10);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenModelInvalid()
    {
        _controller.ModelState.AddModelError("Error", "Error");

        var result = await _controller.Register(new RejestracjaWizytyDTO());

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenServiceFails()
    {
        _mockService.Setup(s => s.ZarejestrujWizyteAsync(It.IsAny<Wizyta>())).ReturnsAsync(false);

        var result = await _controller.Register(new RejestracjaWizytyDTO());

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Nie udało się zarejestrować wizyty.", badRequest.Value);
    }

    [Fact]
    public async Task Register_ReturnsOk_WhenSuccess()
    {
        _mockService.Setup(s => s.ZarejestrujWizyteAsync(It.IsAny<Wizyta>())).ReturnsAsync(true);

        var result = await _controller.Register(new RejestracjaWizytyDTO());

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Wizyta zarejestrowana.", okResult.Value);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdMismatch()
    {
        var wizyta = new Wizyta { Id = 2 };

        RejestracjaWizytyDTO wizDTO = map.WizytaToDTO(wizyta);

        var result = await _controller.Update(1, wizDTO);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenServiceFails()
    {
        var wizyta = new Wizyta { Id = 1 };
        _mockService.Setup(s => s.UpdateWizytaAsync(wizyta)).ReturnsAsync(false);

        RejestracjaWizytyDTO wizDTO = map.WizytaToDTO(wizyta);

        var result = await _controller.Update(1, wizDTO);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenServiceFails()
    {
        _mockService.Setup(s => s.DeleteWizytaAsync(It.IsAny<int>())).ReturnsAsync(false);

        var result = await _controller.Delete(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenSuccess()
    {
        _mockService.Setup(s => s.DeleteWizytaAsync(It.IsAny<int>())).ReturnsAsync(true);

        var result = await _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task AnulujWizyte_ReturnsNotFound_WhenServiceFails()
    {
        _mockService.Setup(s => s.AnulujWizyteAsync(It.IsAny<int>())).ReturnsAsync(false);

        var result = await _controller.AnulujWizyte(1);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Nie znaleziono wizyty.", notFound.Value);
    }

    [Fact]
    public async Task AnulujWizyte_ReturnsOk_WhenSuccess()
    {
        _mockService.Setup(s => s.AnulujWizyteAsync(It.IsAny<int>())).ReturnsAsync(true);

        var result = await _controller.AnulujWizyte(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Wizyta została anulowana.", okResult.Value);
    }

}
