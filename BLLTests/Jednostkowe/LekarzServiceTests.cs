using BLL;
using IDAL_;
using Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BLLTests.Jednostkowe
{
    public class LekarzServiceTests
    {
        private readonly Mock<ILekarzRepository> _mockRepo;
        private readonly LekarzService _service;

        public LekarzServiceTests()
        {
            _mockRepo = new Mock<ILekarzRepository>();
            _service = new LekarzService(_mockRepo.Object);
        }

        private Lekarz GetSampleLekarz()
        {
            return new Lekarz
            {
                Id = 1,
                Imie = "Adam",
                Nazwisko = "Nowak",
                Adres = "ul. Zdrowa 10",
                Email = "adam.nowak@example.com",
                Telefon = "123456789",
                PhoneNumber = "123456789",
                Login = "alekarz",
                Haslo = "tajnehaslo",
                Rola = Rola.Lekarz,
                Specjalizacja = "Kardiolog"
            };
        }

        [Fact]
        public void GetLekarzById_ReturnsCorrectLekarz()
        {
            // Arrange
            var lekarz = GetSampleLekarz();
            _mockRepo.Setup(r => r.GetLekarzById(1)).Returns(lekarz);

            // Act
            var result = _service.GetLekarzById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Adam", result.Imie);
            Assert.Equal("Kardiolog", result.Specjalizacja);
        }

        [Fact]
        public void PobierzWszystkie_ReturnsAllLekarze()
        {
            // Arrange
            var lekarze = new List<Lekarz> { GetSampleLekarz() }.AsQueryable();
            _mockRepo.Setup(r => r.PobierzWszystkie()).Returns(lekarze);

            // Act
            var result = _service.PobierzWszystkie();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void Dodaj_CallsRepositoryWithCorrectLekarz()
        {
            // Arrange
            var lekarz = GetSampleLekarz();

            // Act
            _service.Dodaj(lekarz);

            // Assert
            _mockRepo.Verify(r => r.Dodaj(lekarz), Times.Once);
        }

        [Fact]
        public void Delete_CallsRepositoryWithCorrectId()
        {
            // Act
            _service.Delete(1);

            // Assert
            _mockRepo.Verify(r => r.Delete(1), Times.Once);
        }

        [Fact]
        public void Update_CallsRepositoryWithCorrectLekarz()
        {
            var lekarz = GetSampleLekarz();

            // Act
            _service.Update(lekarz);

            // Assert
            _mockRepo.Verify(r => r.Update(lekarz), Times.Once);
        }

        [Fact]
        public void Save_CallsRepositorySave()
        {
            // Act
            _service.save();

            // Assert
            _mockRepo.Verify(r => r.save(), Times.Once);
        }
    }
}
