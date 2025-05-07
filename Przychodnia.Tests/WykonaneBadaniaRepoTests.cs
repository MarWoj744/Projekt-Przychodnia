using Xunit;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using Models;
using Przychodnia.Repositories;

namespace Przychodnia.Tests
{
    public class WykonaneBadaniaRepoTests
    {
        private DbPrzychodnia GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DbPrzychodnia>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new DbPrzychodnia(options);
        }

        [Fact]
        public void DodajBadanie_DodajePoprawnie()
        {
            // Arrange
            var context = GetInMemoryDbContext(nameof(DodajBadanie_DodajePoprawnie));
            var repo = new WykonaneBadaniaRepository(context);
            var badanie = new WykonaneBadania
            {
                Data = DateTime.Now,
                Wyniki = "Pozytywne",
                WizytaId = 1,
                BadanieId = 1,
                Badanie = new Badanie
                {
                    Id = 1,
                    Nazwa = "Morfologia",
                    Cennik = 100,
                    Specjalizacja = "Diagnostyka"
                }
            };

            // Act
            repo.dodaj(badanie);
            repo.save();

            // Assert
            var result = repo.GetAll().FirstOrDefault();
            Assert.NotNull(result);
            Assert.Equal("Morfologia", result?.Badanie?.Nazwa);
        }

        [Fact]
        public void UsuwanieBadania_UsuwaPoprawnie()
        {
            // Arrange
            var context = GetInMemoryDbContext(nameof(UsuwanieBadania_UsuwaPoprawnie));
            var repo = new WykonaneBadaniaRepository(context);
            var badanie = new WykonaneBadania
            {
                Id = 1,
                Data = DateTime.Now,
                Wyniki = "Pozytywne",
                WizytaId = 1,
                BadanieId = 1,
                Badanie = new Badanie
                {
                    Id = 1,
                    Nazwa = "Morfologia",
                    Cennik = 100,
                    Specjalizacja = "Diagnostyka"
                }
            };

            context.WykonaneBadania.Add(badanie);
            context.SaveChanges();

            // Act
            repo.delete(1);
            repo.save();

            // Assert
            var result = repo.GetWykonaneBadaniaById(1);
            Assert.Null(result);
        }
    }
}
