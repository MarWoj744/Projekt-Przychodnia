using Moq;
using Xunit;
using API.Controllers;
using IBLL;
using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Przychodnia.API.Tests
{
    public class OsobaControllerTest
    {
        private readonly Mock<IOsobaService> _mockService;
        private readonly OsobaController _controller;

        public OsobaControllerTest()
        {
            _mockService = new Mock<IOsobaService>();
            _controller = new OsobaController(_mockService.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithListOfOsoba()
        {
            var osoby = new List<Osoba>
            {
                new Pacjent { Id = 1, Imie = "Jan", Nazwisko = "Kowalski", Email = "jan@gmail.com", Haslo = "haslo", Login = "janek", Rola = Rola.Pacjent, Adres = "bytom" },
                new Lekarz { Id = 2, Imie = "Anna", Nazwisko = "Nowak", Email = "anna@gmail.com", Haslo = "haslo", Login = "anna", Rola = Rola.Lekarz, Adres = "katowice" }
            };

            _mockService.Setup(s => s.PobierzWszystkie()).Returns(osoby.AsQueryable());

            var result = _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Osoba>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public void GetById_ExistingId_ReturnsOkResult()
        {
            var osoba = new Pacjent { Id = 1, Imie = "Jan", Nazwisko = "Kowalski", Email = "jan@gmail.com", Haslo = "haslo", Login = "janek", Rola = Rola.Pacjent, Adres = "bytom" };
            _mockService.Setup(s => s.GetOsobaById(1)).Returns(osoba);

            var result = _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Pacjent>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public void GetById_NotExistingId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetOsobaById(99)).Returns((Osoba)null);

            var result = _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ValidOsoba_ReturnsCreatedAtAction()
        {
            var osoba = new Lekarz
            {
                Id = 3,
                Imie = "Marta",
                Nazwisko = "Zielinska",
                Email = "marta@gmail.com",
                Haslo = "haslo",
                Login = "marta",
                Rola = Rola.Lekarz,
                Adres = "gliwice"
            };

            _mockService.Setup(s => s.ValidateData(osoba)).Returns("Walidacja zakończona sukcesem.");
            _mockService.Setup(s => s.Dodaj(osoba));
            _mockService.Setup(s => s.Save());

            var result = _controller.Create(osoba);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(OsobaController.GetById), createdAtActionResult.ActionName);
            Assert.Equal(osoba.Id, ((Osoba)createdAtActionResult.Value).Id);
        }

        [Fact]
        public void Create_InvalidOsoba_ReturnsBadRequest()
        {
            var osoba = new Pacjent
            {
                Id = 4,
                Imie = "Tomasz",
                Nazwisko = "Bąk",
                Email = "invalid-email",
                Haslo = "haslo",
                Login = "tomasz",
                Rola = Rola.Pacjent,
                Adres = "chorzow"
            };

            _mockService.Setup(s => s.ValidateData(osoba)).Returns("Nieprawidłowy email");

            var result = _controller.Create(osoba);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Nieprawidłowy email", badRequestResult.Value);
        }

        [Fact]
        public void Update_ValidOsoba_ReturnsNoContent()
        {
            var osoba = new Lekarz
            {
                Id = 5,
                Imie = "Adam",
                Nazwisko = "Kowal",
                Email = "adam@gmail.com",
                Haslo = "haslo",
                Login = "adam",
                Rola = Rola.Lekarz,
                Adres = "sosnowiec"
            };

            _mockService.Setup(s => s.GetOsobaById(osoba.Id)).Returns(osoba);
            _mockService.Setup(s => s.Update(osoba));
            _mockService.Setup(s => s.Save());

            var result = _controller.Update(osoba.Id, osoba);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Update_InvalidId_ReturnsBadRequest()
        {
            var osoba = new Pacjent
            {
                Id = 6,
                Imie = "Ewa",
                Nazwisko = "Nowak",
                Email = "ewa@gmail.com",
                Haslo = "haslo",
                Login = "ewa",
                Rola = Rola.Pacjent,
                Adres = "zabrze"
            };

            var result = _controller.Update(999, osoba);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Niepoprawne dane.", badRequestResult.Value);
        }

        [Fact]
        public void Update_NotFound_ReturnsNotFound()
        {
            var osoba = new Lekarz
            {
                Id = 7,
                Imie = "Piotr",
                Nazwisko = "Malinowski",
                Email = "piotr@gmail.com",
                Haslo = "haslo",
                Login = "piotr",
                Rola = Rola.Lekarz,
                Adres = "myslowice"
            };

            _mockService.Setup(s => s.GetOsobaById(osoba.Id)).Returns((Osoba)null);

            var result = _controller.Update(osoba.Id, osoba);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ExistingId_ReturnsNoContent()
        {
            var osoba = new Pacjent
            {
                Id = 8,
                Imie = "Kasia",
                Nazwisko = "Kowalska",
                Email = "kasia@gmail.com",
                Haslo = "haslo",
                Login = "kasia",
                Rola = Rola.Pacjent,
                Adres = "myszkow"
            };

            _mockService.Setup(s => s.GetOsobaById(osoba.Id)).Returns(osoba);
            _mockService.Setup(s => s.Delete(osoba.Id));
            _mockService.Setup(s => s.Save());

            var result = _controller.Delete(osoba.Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_NotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetOsobaById(999)).Returns((Osoba)null);

            var result = _controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
