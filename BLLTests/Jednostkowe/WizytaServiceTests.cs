using BLL;
using DTOs;
using IDAL_;
using Models;
using Models.Mapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BLLTests.Jednostkowe
{
    public class WizytaServiceTests
    {
        private readonly Mock<IWizytaRepository> _mockRepo;
        private readonly WizytaService _service;
        private readonly Mapper map;

        public WizytaServiceTests()
        {
            _mockRepo = new Mock<IWizytaRepository>();
            _service = new WizytaService(_mockRepo.Object);
            map = new Mapper();
        }

        private List<Wizyta> GetFakeWizyty()
        {
            return new List<Wizyta>
            {
                new Wizyta { Id = 1, PacjentId = 1, LekarzId = 1, RecepcjonistkaId = 1, Data = DateTime.Now.AddDays(1), Opis = "Opis 1" },
                new Wizyta { Id = 2, PacjentId = 2, LekarzId = 2, RecepcjonistkaId = 2, Data = DateTime.Now.AddDays(2), Opis = "Opis 2" }
            };
        }

        [Fact]
        public async Task ZarejestrujWizyteAsync_PoprawneDane_ZwracaTrue()
        {
            // Arrange
            var dto = new RejestracjaWizytyDTO
            {
                PacjentId = 1,
                LekarzId = 1,
                RecepcjonistkaId = 1,
                DataWizyty = DateTime.Now.AddDays(1),
                Opis = "Testowa wizyta"
            };

            // Act
            Wizyta wiz = map.WizytaToEntity(dto);
            var result = await _service.ZarejestrujWizyteAsync(wiz);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.dodaj(It.IsAny<Wizyta>()), Times.Once);
            _mockRepo.Verify(r => r.save(), Times.Once);
        }

        [Fact]
        public async Task ZarejestrujWizyteAsync_NieprawidlowaData_RzucaWyjatek()
        {
            // Arrange
            var dto = new RejestracjaWizytyDTO
            {
                PacjentId = 1,
                LekarzId = 1,
                RecepcjonistkaId = 1,
                DataWizyty = DateTime.Now.AddDays(-1),
                Opis = "Nieprawidlowa wizyta"
            };

            Wizyta wiz = map.WizytaToEntity(dto);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.ZarejestrujWizyteAsync(wiz));
            _mockRepo.Verify(r => r.dodaj(It.IsAny<Wizyta>()), Times.Never);
            _mockRepo.Verify(r => r.save(), Times.Never);
        }

        [Fact]
        public void GetAll_ZwracaWszystkieWizyty()
        {
            // Arrange
            var fakeWizyty = GetFakeWizyty().AsQueryable();
            _mockRepo.Setup(r => r.PobierzWszystkie()).Returns(fakeWizyty);

            // Act
            var result = _service.GetAll().ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Opis 1", result[0].Opis);
            Assert.Equal("Opis 2", result[1].Opis);
        }

        [Fact]
        public void GetWizytaById_ZwracaWizyte()
        {
            // Arrange
            var wizyta = new Wizyta { Id = 1, Opis = "Wizyta testowa" };
            _mockRepo.Setup(r => r.getWizytaById(1)).Returns(wizyta);

            // Act
            var result = _service.GetWizytaById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Wizyta testowa", result.Opis);
        }

        [Fact]
        public async Task UpdateWizytaAsync_AktualizujeWizyte()
        {
            // Arrange
            var wizyta = new Wizyta { Id = 1, Opis = "Zmieniona wizyta" };

            // Act
            var result = await _service.UpdateWizytaAsync(wizyta);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.update(wizyta), Times.Once);
            _mockRepo.Verify(r => r.save(), Times.Once);
        }

        [Fact]
        public async Task DeleteWizytaAsync_UsuwaWizyte()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _service.DeleteWizytaAsync(id);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.delete(id), Times.Once);
            _mockRepo.Verify(r => r.save(), Times.Once);
        }
    }
}
