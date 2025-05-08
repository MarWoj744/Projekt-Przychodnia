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

        [Fact]
        public void GetWykonaneBadaniaById_ZwracaPoprawneBadanie()
        {
            // Arrange
            var context = GetInMemoryDbContext(nameof(GetWykonaneBadaniaById_ZwracaPoprawneBadanie));
            var badanie = new WykonaneBadania
            {
                Id = 1,
                Data = DateTime.Now,
                Wyniki = "Negatywne",
                WizytaId = 1,
                BadanieId = 1,
                Badanie = new Badanie
                {
                    Id = 1,
                    Nazwa = "USG",
                    Cennik = 150,
                    Specjalizacja = "Radiologia"
                }
            };

            context.WykonaneBadania.Add(badanie);
            context.SaveChanges();

            var repo = new WykonaneBadaniaRepository(context);

            // Act
            var result = repo.GetWykonaneBadaniaById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("USG", result.Badanie.Nazwa);
        }

        [Fact]
        public void UpdateBadania_AktualizujePoprawnie()
        {
            // Arrange
            var context = GetInMemoryDbContext(nameof(UpdateBadania_AktualizujePoprawnie));
            var badanie = new WykonaneBadania
            {
                Id = 1,
                Data = DateTime.Now,
                Wyniki = "Negatywne",
                WizytaId = 1,
                BadanieId = 1,
                Badanie = new Badanie
                {
                    Id = 1,
                    Nazwa = "EKG",
                    Cennik = 120,
                    Specjalizacja = "Kardiologia"
                }
            };

            context.WykonaneBadania.Add(badanie);
            context.SaveChanges();

            var repo = new WykonaneBadaniaRepository(context);

            // Act
            badanie.Wyniki = "Pozytywne";
            repo.update(badanie);
            repo.save();

            var result = repo.GetWykonaneBadaniaById(1);

            // Assert
            Assert.Equal("Pozytywne", result.Wyniki);
        }

        [Fact]
        public void GetAll_ZwracaWszystkieBadania()
        {
            // Arrange
            var context = GetInMemoryDbContext(nameof(GetAll_ZwracaWszystkieBadania));
            context.WykonaneBadania.Add(new WykonaneBadania { Id = 1, Wyniki = "W1", WizytaId = 1, BadanieId = 1 });
            context.WykonaneBadania.Add(new WykonaneBadania { Id = 2, Wyniki = "W2", WizytaId = 2, BadanieId = 2 });
            context.SaveChanges();

            var repo = new WykonaneBadaniaRepository(context);

            // Act
            var result = repo.GetAll().ToList();

            // Assert
            Assert.Equal(2, result.Count);
        }

    }
}
