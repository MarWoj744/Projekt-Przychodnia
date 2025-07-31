using BLL;
using IDAL_;
using Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BLLTests.Jednostkowe
{
    public class RecepcjonistkaServiceTests
    {
        private readonly Mock<IRecepcjonistkaRepository> _mockRepo;
        private readonly RecepcjonistkaService _service;

        public RecepcjonistkaServiceTests()
        {
            _mockRepo = new Mock<IRecepcjonistkaRepository>();
            _service = new RecepcjonistkaService(_mockRepo.Object);
        }

        private List<Recepcjonistka> GetFakeRecepcjonistki()
        {
            return new List<Recepcjonistka>
            {
                new Recepcjonistka { Id = 1, Imie = "Anna", Nazwisko = "Kowalska" },
                new Recepcjonistka { Id = 2, Imie = "Maria", Nazwisko = "Nowak" }
            };
        }

        [Fact]
        public void PobierzWszystkie_ZwracaListeRecepcjonistek()
        {
            // Arrange
            var lista = GetFakeRecepcjonistki().AsQueryable();
            _mockRepo.Setup(r => r.PobierzWszystkie()).Returns(lista);

            // Act
            var result = _service.PobierzWszystkie().ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Anna", result[0].Imie);
            Assert.Equal("Maria", result[1].Imie);
        }

        [Fact]
        public void GetRecepcjonistkaById_ZwracaPoprawnaRecepcjonistke()
        {
            // Arrange
            var recepcjonistka = new Recepcjonistka { Id = 1, Imie = "Anna" };
            _mockRepo.Setup(r => r.GetRecepcjonistkaById(1)).Returns(recepcjonistka);

            // Act
            var result = _service.GetRecepcjonistkaById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Anna", result.Imie);
        }

        [Fact]
        public void Dodaj_WywolujeDodajWRepozytorium()
        {
            // Arrange
            var recepcjonistka = new Recepcjonistka { Id = 3, Imie = "Joanna" };

            // Act
            _service.Dodaj(recepcjonistka);

            // Assert
            _mockRepo.Verify(r => r.Dodaj(recepcjonistka), Times.Once);
        }

        [Fact]
        public void Delete_WywolujeDeleteWRepozytorium()
        {
            // Arrange
            int id = 2;

            // Act
            _service.Delete(id);

            // Assert
            _mockRepo.Verify(r => r.Delete(id), Times.Once);
        }

        [Fact]
        public void Update_WywolujeUpdateWRepozytorium()
        {
            // Arrange
            var recepcjonistka = new Recepcjonistka { Id = 1, Imie = "Anna", Nazwisko = "Nowa" };

            // Act
            _service.Update(recepcjonistka);

            // Assert
            _mockRepo.Verify(r => r.Update(recepcjonistka), Times.Once);
        }

        [Fact]
        public void Save_WywolujeSaveWRepozytorium()
        {
            // Act
            _service.save();

            // Assert
            _mockRepo.Verify(r => r.save(), Times.Once);
        }
    }
}
