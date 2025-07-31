using BLL;
using IDAL_;
using Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BLLTests.Jednostkowe
{
    public class PacjentServiceTests
    {
        private readonly Mock<IPacjentRepository> _mockRepo;
        private readonly PacjentService _service;

        public PacjentServiceTests()
        {
            _mockRepo = new Mock<IPacjentRepository>();
            _service = new PacjentService(_mockRepo.Object);
        }

        private List<Pacjent> GetFakePacjenci()
        {
            return new List<Pacjent>
            {
                new Pacjent { Id = 1, Imie = "Jan", Nazwisko = "Kowalski", PESEL = "44051401359" },
                new Pacjent { Id = 2, Imie = "Anna", Nazwisko = "Nowak", PESEL = "50010112345" }
            };
        }

        [Fact]
        public void PobierzWszystkie_ZwracaListePacjentow()
        {
            var pacjenci = GetFakePacjenci().AsQueryable();
            _mockRepo.Setup(r => r.PobierzWszystkie()).Returns(pacjenci);

            var result = _service.PobierzWszystkie().ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("Jan", result[0].Imie);
        }

        [Fact]
        public void GetPacjentById_ZwracaPoprawnegoPacjenta()
        {
            var pacjent = new Pacjent { Id = 1, Imie = "Jan" };
            _mockRepo.Setup(r => r.GetPacjentById(1)).Returns(pacjent);

            var result = _service.GetPacjentById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Dodaj_WywolujeDodajWRepozytorium()
        {
            var pacjent = new Pacjent { Id = 3, Imie = "Kasia" };

            _service.Dodaj(pacjent);

            _mockRepo.Verify(r => r.Dodaj(pacjent), Times.Once);
        }

        [Fact]
        public void Delete_WywolujeDeleteWRepozytorium()
        {
            _service.Delete(1);

            _mockRepo.Verify(r => r.Delete(1), Times.Once);
        }

        [Fact]
        public void Update_WywolujeUpdateWRepozytorium()
        {
            var pacjent = new Pacjent { Id = 2, Imie = "Anna" };

            _service.Update(pacjent);

            _mockRepo.Verify(r => r.Update(pacjent), Times.Once);
        }

        [Fact]
        public void Save_WywolujeSaveWRepozytorium()
        {
            _service.save();

            _mockRepo.Verify(r => r.save(), Times.Once);
        }

        [Theory]
        [InlineData("44051401359", "PESEL jest poprawny.")]
        [InlineData("12345678901", "Nieprawidłowy numer PESEL.")]
        [InlineData("", "PESEL nie może być pusty.")]
        [InlineData(null, "PESEL nie może być pusty.")]
        public void ValidatePesel_ZwracaOdpowiedniKomunikat(string pesel, string expected)
        {
            var pacjent = new Pacjent { PESEL = pesel };

            var result = _service.ValidatePesel(pacjent);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("44051401359", true)]  // poprawny
        [InlineData("12345678901", false)] // błędny
        [InlineData("abcdefghijk", false)] // nie numeryczny
        [InlineData("12345", false)]       // za krótki
        public void IsValidPesel_ZwracaPoprawnaWartosc(string pesel, bool expected)
        {
            var result = _service.IsValidPesel(pesel);

            Assert.Equal(expected, result);
        }
    }
}
