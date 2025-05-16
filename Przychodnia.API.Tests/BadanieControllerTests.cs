using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using IBLL;
using Models;
using Przychodnia.API.Controllers;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Controllers;

namespace Przychodnia.API.Tests
{
    public class BadanieControllerTest
    {
        private readonly Mock<IBadanieService> _mockService;
        private readonly BadanieController _controller;

        public BadanieControllerTest()
        {
            _mockService = new Mock<IBadanieService>();
            _controller = new BadanieController(_mockService.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithListOfBadania()
        {
            var badania = new List<Badanie>
            {
                new Badanie { Id = 1, Nazwa = "Badanie A" },
                new Badanie { Id = 2, Nazwa = "Badanie B" }
            }.AsQueryable();

            _mockService.Setup(s => s.PobierzWszystkie()).Returns(badania);

            var result = _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBadania = Assert.IsAssignableFrom<IEnumerable<Badanie>>(okResult.Value);
            Assert.Equal(2, returnBadania.Count());
        }

        [Fact]
        public void GetById_ExistingId_ReturnsOkResult_WithBadanie()
        {
            var badanie = new Badanie { Id = 1, Nazwa = "Badanie A" };
            _mockService.Setup(s => s.GetBadanieById(1)).Returns(badanie);

            var result = _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBadanie = Assert.IsType<Badanie>(okResult.Value);
            Assert.Equal("Badanie A", returnBadanie.Nazwa);
        }

        [Fact]
        public void GetById_NonExistingId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetBadanieById(99)).Returns((Badanie)null);

            var result = _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ValidBadanie_ReturnsCreatedAtAction()
        {
            var badanie = new Badanie { Id = 1, Nazwa = "Badanie A" };

            var result = _controller.Create(badanie);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnBadanie = Assert.IsType<Badanie>(createdAtActionResult.Value);
            Assert.Equal("Badanie A", returnBadanie.Nazwa);
        }

        [Fact]
        public void Update_ExistingId_ReturnsNoContent()
        {
            var badanie = new Badanie { Id = 1, Nazwa = "Badanie A" };
            _mockService.Setup(s => s.GetBadanieById(1)).Returns(badanie);

            var result = _controller.Update(1, badanie);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Update_NonExistingId_ReturnsNotFound()
        {
            var badanie = new Badanie { Id = 99, Nazwa = "Badanie X" };
            _mockService.Setup(s => s.GetBadanieById(99)).Returns((Badanie)null);

            var result = _controller.Update(99, badanie);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ExistingId_ReturnsNoContent()
        {
            var badanie = new Badanie { Id = 1, Nazwa = "Badanie A" };
            _mockService.Setup(s => s.GetBadanieById(1)).Returns(badanie);

            var result = _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_NonExistingId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetBadanieById(99)).Returns((Badanie)null);

            var result = _controller.Delete(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
