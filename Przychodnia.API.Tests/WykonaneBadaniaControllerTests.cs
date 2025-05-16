using IBLL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using DTOs;
using Przychodnia.API.Controllers;
using System.Collections.Generic;
using Xunit;
using System.Linq;

public class WykonaneBadaniaControllerTests
{
    private readonly Mock<IWykonaneBadanieService> _mockService;
    private readonly WykonaneBadaniaController _controller;

    public WykonaneBadaniaControllerTests()
    {
        _mockService = new Mock<IWykonaneBadanieService>();
        _controller = new WykonaneBadaniaController(_mockService.Object);
    }

    [Fact]
    public void GetAll_ReturnsOk_WithList()
    {
        var list = new List<WykonaneBadaniaDTO>
        {
            new WykonaneBadaniaDTO { BadanieId = 1 },
            new WykonaneBadaniaDTO { BadanieId = 2 }
        };
        _mockService.Setup(s => s.GetAll()).Returns(list);

        var result = _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<WykonaneBadaniaDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenNull()
    {
        _mockService.Setup(s => s.GetById(It.IsAny<int>())).Returns((WykonaneBadaniaDTO)null);

        var result = _controller.GetById(10);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_ReturnsBadRequest_WhenModelInvalid()
    {
        _controller.ModelState.AddModelError("Error", "Error");
        var dto = new WykonaneBadaniaDTO();

        var result = _controller.Create(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction_WhenValid()
    {
        var dto = new WykonaneBadaniaDTO { BadanieId = 1 };

        var result = _controller.Create(dto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(dto, createdResult.Value);
    }

    [Fact]
    public void Update_ReturnsBadRequest_WhenIdMismatch()
    {
        var dto = new WykonaneBadaniaDTO { BadanieId = 2 };

        var result = _controller.Update(1, dto);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNoContent()
    {
        var result = _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
        _mockService.Verify(s => s.Delete(1), Times.Once);
        _mockService.Verify(s => s.Save(), Times.Once);
    }
}
