using IBLL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Przychodnia.API.Controllers;
using DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class HarmonogramControllerTests
{
    private readonly Mock<IHarmonogramService> _mockService;
    private readonly HarmonogramController _controller;

    public HarmonogramControllerTests()
    {
        _mockService = new Mock<IHarmonogramService>();
        _controller = new HarmonogramController(_mockService.Object);
    }

    [Fact]
    public void GetAll_ReturnsOk_WithList()
    {
        var list = new List<Harmonogram> { new Harmonogram { Id = 1 } }.AsQueryable();
        _mockService.Setup(s => s.PobierzWszystkie()).Returns(list);

        var result = _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IQueryable<Harmonogram>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenNull()
    {
        _mockService.Setup(s => s.GetHarmonogramById(It.IsAny<int>())).Returns((Harmonogram)null);

        var result = _controller.GetById(5);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction()
    {
        var dto = new HarmonogramDTO
        {
            Id = 1,
            LekarzId = 2,
            DataOd = DateTime.Now,
            DataDo = DateTime.Now.AddHours(2),
            Opis = "test"
        };

        var result = _controller.Create(dto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetById", createdResult.ActionName);
    }

    [Fact]
    public void Update_ReturnsBadRequest_WhenIdMismatch()
    {
        var dto = new HarmonogramDTO { Id = 2 };
        var result = _controller.Update(1, dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNotFound_WhenNotExists()
    {
        _mockService.Setup(s => s.GetHarmonogramById(It.IsAny<int>())).Returns((Harmonogram)null);

        var result = _controller.Delete(1);

        Assert.IsType<NotFoundResult>(result);
    }
}
