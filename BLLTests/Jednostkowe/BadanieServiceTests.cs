using BLL;
using IDAL_;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BLLTests.Jednostkowe
{
    public class BadanieServiceTests
    {
        private readonly Mock<IBadanieRepository> _mockRepo;
        private readonly BadanieService _service;

        public BadanieServiceTests()
        {
            _mockRepo = new Mock<IBadanieRepository>();
            _service = new BadanieService(_mockRepo.Object);
        }

        private Badanie GetSampleBadanie()
        {
            return new Badanie
            {
                Id = 1,
                Nazwa = "Morfologia",
                Cennik = 50.0m,
                Specjalizacja = "Diagnostyka",
                Wykonane = new List<WykonaneBadania>()
            };
        }

        [Fact]
        public void GetBadanieById_ReturnsCorrectBadanie()
        {
            // Arrange
            var badanie = GetSampleBadanie();
            _mockRepo.Setup(r => r.GetBadanieById(1)).Returns(badanie);

            // Act
            var result = _service.GetBadanieById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Morfologia", result.Nazwa);
            Assert.Equal("Diagnostyka", result.Specjalizacja);
        }

        [Fact]
        public void PobierzWszystkie_ReturnsAllBadania()
        {
            // Arrange
            var badania = new List<Badanie> { GetSampleBadanie() }.AsQueryable();
            _mockRepo.Setup(r => r.PobierzWszystkie()).Returns(badania);

            // Act
            var result = _service.PobierzWszystkie();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void Dodaj_CallsRepositoryWithCorrectBadanie()
        {
            var badanie = GetSampleBadanie();

            // Act
            _service.Dodaj(badanie);

            // Assert
            _mockRepo.Verify(r => r.Dodaj(badanie), Times.Once);
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
        public void Update_CallsRepositoryWithCorrectBadanie()
        {
            var badanie = GetSampleBadanie();

            // Act
            _service.Update(badanie);

            // Assert
            _mockRepo.Verify(r => r.Update(badanie), Times.Once);
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
