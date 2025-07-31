using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Przychodnia.API.Controllers;
using IBLL;
using Models;
using System.Collections.Generic;
using System.Linq;
using Models.Mapper;
using DTOs;

namespace Przychodnia.API.Tests
{
    public class LekarzControllerTest
    {
        private readonly Mock<ILekarzService> _mockService;
        private readonly LekarzController _controller;
        private readonly Mapper map;

        public LekarzControllerTest()
        {
            _mockService = new Mock<ILekarzService>();
            _controller = new LekarzController(_mockService.Object);
            map = new Mapper();
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithListOfLekarze()
        {
            var lekarze = new List<Lekarz>
            {
                new Lekarz { Id = 1, Imie = "Jan", Nazwisko = "Kowalski" },
                new Lekarz { Id = 2, Imie = "Anna", Nazwisko = "Nowak" }
            }.AsQueryable();

            _mockService.Setup(s => s.PobierzWszystkie()).Returns(lekarze);

            var result = _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnLekarze = Assert.IsAssignableFrom<List<Lekarz>>(okResult.Value);
            Assert.Equal(2, returnLekarze.Count);
        }

        [Fact]
        public void GetById_ExistingId_ReturnsOkResult_WithLekarz()
        {
            var lekarz = new Lekarz { Id = 1, Imie = "Jan", Nazwisko = "Kowalski" };
            _mockService.Setup(s => s.GetLekarzById(1)).Returns(lekarz);

            var result = _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnLekarz = Assert.IsType<Lekarz>(okResult.Value);
            Assert.Equal("Jan", returnLekarz.Imie);
            Assert.Equal("Kowalski", returnLekarz.Nazwisko);
        }

        [Fact]
        public void GetById_NonExistingId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetLekarzById(99)).Returns((Lekarz)null);

            var result = _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ValidLekarz_ReturnsCreatedAtAction()
        {
            var lekarz = new Lekarz { Id = 1, Imie = "Jan", Nazwisko = "Kowalski" };

            LekarzDTO lDTO = map.LekarzToDTO(lekarz);

            var result = _controller.Create(lDTO);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnLekarz = Assert.IsType<Lekarz>(createdAtActionResult.Value);
            Assert.Equal("Jan", returnLekarz.Imie);
            Assert.Equal("Kowalski", returnLekarz.Nazwisko);
            _mockService.Verify(s => s.Dodaj(lekarz), Times.Once);
            _mockService.Verify(s => s.save(), Times.Once);
        }

        [Fact]
        public void Create_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Imie", "Required");
            var lekarz = new Lekarz();

            LekarzDTO lDTO = map.LekarzToDTO(lekarz);

            var result = _controller.Create(lDTO);

            Assert.IsType<BadRequestObjectResult>(result);
            _mockService.Verify(s => s.Dodaj(It.IsAny<Lekarz>()), Times.Never);
            _mockService.Verify(s => s.save(), Times.Never);
        }

        [Fact]
        public void Update_ExistingId_ReturnsNoContent()
        {
            var lekarz = new Lekarz { Id = 1, Imie = "Jan", Nazwisko = "Kowalski" };
            _mockService.Setup(s => s.GetLekarzById(1)).Returns(lekarz);

            LekarzDTO lDTO = map.LekarzToDTO(lekarz);

            var result = _controller.Update(1, lDTO);

            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.Update(lekarz), Times.Once);
            _mockService.Verify(s => s.save(), Times.Once);
        }

        [Fact]
        public void Update_IdMismatch_ReturnsBadRequest()
        {
            var lekarz = new Lekarz { Id = 2, Imie = "Jan", Nazwisko = "Kowalski" };

            LekarzDTO lDTO = map.LekarzToDTO(lekarz);

            var result = _controller.Update(1, lDTO);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID w URL i obiekcie się różnią", badRequest.Value);
            _mockService.Verify(s => s.Update(It.IsAny<Lekarz>()), Times.Never);
            _mockService.Verify(s => s.save(), Times.Never);
        }

        [Fact]
        public void Update_NonExistingId_ReturnsNotFound()
        {
            var lekarz = new Lekarz { Id = 1, Imie = "Jan", Nazwisko = "Kowalski" };
            _mockService.Setup(s => s.GetLekarzById(1)).Returns((Lekarz)null);

            LekarzDTO lDTO = map.LekarzToDTO(lekarz);

            var result = _controller.Update(1, lDTO);

            Assert.IsType<NotFoundResult>(result);
            _mockService.Verify(s => s.Update(It.IsAny<Lekarz>()), Times.Never);
            _mockService.Verify(s => s.save(), Times.Never);
        }

        [Fact]
        public void Delete_ExistingId_ReturnsNoContent()
        {
            var lekarz = new Lekarz { Id = 1, Imie = "Jan", Nazwisko = "Kowalski" };
            _mockService.Setup(s => s.GetLekarzById(1)).Returns(lekarz);

            var result = _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.Delete(1), Times.Once);
            _mockService.Verify(s => s.save(), Times.Once);
        }

        [Fact]
        public void Delete_NonExistingId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetLekarzById(99)).Returns((Lekarz)null);

            var result = _controller.Delete(99);

            Assert.IsType<NotFoundResult>(result);
            _mockService.Verify(s => s.Delete(It.IsAny<int>()), Times.Never);
            _mockService.Verify(s => s.save(), Times.Never);
        }
    }
}
