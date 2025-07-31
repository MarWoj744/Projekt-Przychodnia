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

public class RecepcjonistkaControllerTests
{
    private readonly Mock<IRecepcjonistkaService> _mockService;
    private readonly RecepcjonistkaController _controller;
    private readonly Mapper map;

    public RecepcjonistkaControllerTests()
    {
        _mockService = new Mock<IRecepcjonistkaService>();
        _controller = new RecepcjonistkaController(_mockService.Object);
        map = new Mapper();
    }

    [Fact]
    public void GetAll_ReturnsOk_WithList()
    {
        var list = new List<Recepcjonistka>
        {
            new Recepcjonistka { Id = 1 },
            new Recepcjonistka { Id = 2 }
        }.AsQueryable();

        _mockService.Setup(s => s.PobierzWszystkie()).Returns(list);

        var result = _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IQueryable<Recepcjonistka>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenNull()
    {
        _mockService.Setup(s => s.GetRecepcjonistkaById(It.IsAny<int>())).Returns((Recepcjonistka)null);

        var result = _controller.GetById(10);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_ReturnsBadRequest_WhenModelInvalid()
    {
        _controller.ModelState.AddModelError("Error", "Error");
        var obj = new RecepcjonistkaDTO();

        var result = _controller.Create(obj);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction_WhenValid()
    {
        var obj = new RecepcjonistkaDTO { Id = 1 };

        var result = _controller.Create(obj);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdRecepcjonistka = Assert.IsType<Recepcjonistka>(createdResult.Value);
        Assert.Equal(obj.Id, createdRecepcjonistka.Id);
    }

    [Fact]
    public void Update_ReturnsBadRequest_WhenIdMismatch()
    {
        var obj = new RecepcjonistkaDTO { Id = 2 };

        var result = _controller.Update(1, obj);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNoContent()
    {
        var result = _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
        _mockService.Verify(s => s.Delete(1), Times.Once);
        _mockService.Verify(s => s.save(), Times.Once);
    }
}
