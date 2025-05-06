using Xunit;
using Microsoft.EntityFrameworkCore;

using Przychodnia.Repositories;
using System.Linq;
using Models;

namespace Przychodnia.Tests
{
    public class OsobaRepositoryTests
    {
        private DbPrzychodnia GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DbPrzychodnia>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new DbPrzychodnia(options);
        }

        [Fact]
        public void DodajOsobe_DzialaPoprawnie()
        {
            // Arrange
            var context = GetInMemoryDbContext(nameof(DodajOsobe_DzialaPoprawnie));
            var repo = new OsobaRepository(context);
            var osoba = new Pacjent
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Adres = "ul. Przykładowa 1",
                Email = "jan@example.com",
                Telefon = "123456789",
                Login = "jan.kowalski",
                Haslo = "haslo123",
                Rola = Rola.Pacjent,
                PhoneNumber = "123456789",
                PESEL = "12345678901" 
            };

            // Act
            repo.Dodaj(osoba);
            repo.save();

            // Assert
            var result = repo.GetOsobaById(1);
            Assert.NotNull(result);
            Assert.Equal("test", result.Login);
        }

        [Fact]
        public void UsunOsobe_UsuwaPoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UsunOsobe_UsuwaPoprawnie));
            var osoba = new Pacjent
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Adres = "ul. Przykładowa 1",
                Email = "jan@example.com",
                Telefon = "123456789",
                Login = "jan.kowalski",
                Haslo = "haslo123",
                Rola = Rola.Pacjent,
                PhoneNumber = "123456789",
                PESEL = "12345678901" 
            };
            context.Osoby.Add(osoba);
            context.SaveChanges();

            var repo = new OsobaRepository(context);

            // Act
            repo.Delete(2);
            
            repo.save();

            // Assert
            var result = repo.GetOsobaById(2);
            Assert.Null(result);
        }
    }
}
