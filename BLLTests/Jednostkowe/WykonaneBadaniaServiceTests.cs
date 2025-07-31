using BLL;
using DTOs;
using IDAL_;
using Models;
using Models.Mapper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BLLTests.Jednostkowe
{
    public class WykonaneBadaniaServiceTests
    {
        private Mock<IWykonaneBadaniaRepository> _mockBadaniaRepo;
        private Mock<IWizytaRepository> _mockWiztaRepo;
        private WykonaneBadaniaService _service;
        private readonly Mapper map;

        public WykonaneBadaniaServiceTests()
        {
            _mockBadaniaRepo = new Mock<IWykonaneBadaniaRepository>();
            _mockWiztaRepo = new Mock<IWizytaRepository>();
            _service = new WykonaneBadaniaService(_mockBadaniaRepo.Object, _mockWiztaRepo.Object);
            map = new Mapper();
        }

        private List<WykonaneBadania> GetFakeWykonaneBadania()
        {
            return new List<WykonaneBadania>
            {
                new WykonaneBadania { WizytaId = 1, BadanieId = 1, Data = DateTime.Now, Wyniki = "Wynik 1" },
                new WykonaneBadania { WizytaId = 2, BadanieId = 2, Data = DateTime.Now, Wyniki = "Wynik 2" }
            };
        }

        [Fact]
        public void GetAll_ZwracaWszystkieBadania()
        {
            // Arrange
            var fakeBadania = GetFakeWykonaneBadania();
            _mockBadaniaRepo.Setup(repo => repo.GetAll()).Returns(fakeBadania.AsQueryable());

            // Act
            var result = _service.GetAll().ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Wynik 1", result[0].Wyniki);
            Assert.Equal("Wynik 2", result[1].Wyniki);
        }

        [Fact]
        public void GetById_ZwracaBadaniePoId()
        {
            // Arrange
            var badanie = new WykonaneBadania { WizytaId = 1, BadanieId = 1, Data = DateTime.Now, Wyniki = "Wynik 1" };
            _mockBadaniaRepo.Setup(repo => repo.GetWykonaneBadaniaById(1)).Returns(badanie);

            // Act
            var result = _service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.BadanieId);
            Assert.Equal("Wynik 1", result.Wyniki);
        }

        [Fact]
        public void Dodaj_DodajeBadanie()
        {
            // Arrange
            var dto = new WykonaneBadaniaDTO { WizytaId = 1, BadanieId = 1, Data = DateTime.Now, Wyniki = "Wynik 1" };

            WykonaneBadania wyk = new WykonaneBadania();
            wyk = map.WykonaneBadaniaToEntity(dto);
            // Act
            _service.Dodaj(wyk);

            // Assert
            _mockBadaniaRepo.Verify(repo => repo.dodaj(It.IsAny<WykonaneBadania>()), Times.Once);
        }

        [Fact]
        public void Update_aktualizujeBadanie()
        {
            // Arrange
            var existingBadanie = new WykonaneBadania { WizytaId = 1, BadanieId = 1, Data = DateTime.Now, Wyniki = "Wynik 1" };
            _mockBadaniaRepo.Setup(repo => repo.GetAll()).Returns(new List<WykonaneBadania> { existingBadanie }.AsQueryable());

            var dto = new WykonaneBadaniaDTO { WizytaId = 1, BadanieId = 1, Data = DateTime.Now.AddDays(1), Wyniki = "Zaktualizowany wynik" };

            WykonaneBadania wyk = map.WykonaneBadaniaToEntity(dto);
            // Act
            _service.Update(wyk);

            // Assert
            Assert.Equal("Zaktualizowany wynik", existingBadanie.Wyniki);
            _mockBadaniaRepo.Verify(repo => repo.update(existingBadanie), Times.Once);
        }

        [Fact]
        public void Delete_usuwaBadanie()
        {
            // Arrange
            int badanieId = 1;

            // Act
            _service.Delete(badanieId);

            // Assert
            _mockBadaniaRepo.Verify(repo => repo.delete(badanieId), Times.Once);
        }

        [Fact]
        public void Save_ZapisujeZmiany()
        {
            // Act
            _service.Save();

            // Assert
            _mockBadaniaRepo.Verify(repo => repo.save(), Times.Once);
        }
    }
}