using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Models;
using DAL;

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
                Id = 1,
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Adres = "ul. Przykładowa 1",
                Email = "jan@example.com",
                Telefon = "123456789",
                Login = "test",
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
                Id = 1,
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Adres = "ul. Przykładowa 1",
                Email = "jan@example.com",
                Telefon = "123456789",
                Login = "test",
                Haslo = "haslo123",
                Rola = Rola.Pacjent,
                PhoneNumber = "123456789",
                PESEL = "12345678901" 
            };
            context.Osoby.Add(osoba);
            context.SaveChanges();

            var repo = new OsobaRepository(context);

            // Act
            repo.Delete(1);
            
            repo.save();

            // Assert
            var result = repo.GetOsobaById(1);
            Assert.Null(result);
        }
        [Fact]
        public void GetOsobaByLogin_ZwracaPoprawnaOsobe()
        {
            var context = GetInMemoryDbContext(nameof(GetOsobaByLogin_ZwracaPoprawnaOsobe));
            var osoba = new Pacjent { Id = 1, Login = "login1", Email = "a@a.pl", PhoneNumber = "111", Rola = Rola.Pacjent };
            context.Osoby.Add(osoba);
            context.SaveChanges();

            var repo = new OsobaRepository(context);
            var result = repo.GetOsobaByLogin("login1");

            Assert.NotNull(result);
            Assert.Equal("login1", result.Login);
        }

        [Fact]
        public void GetOsobaByEmail_ZwracaPoprawnaOsobe()
        {
            var context = GetInMemoryDbContext(nameof(GetOsobaByEmail_ZwracaPoprawnaOsobe));
            var osoba = new Pacjent { Id = 1, Login = "login1", Email = "email@example.com", PhoneNumber = "111", Rola = Rola.Pacjent };
            context.Osoby.Add(osoba);
            context.SaveChanges();

            var repo = new OsobaRepository(context);
            var result = repo.GetOsobaByEmail("email@example.com");

            Assert.NotNull(result);
            Assert.Equal("email@example.com", result.Email);
        }

        [Fact]
        public void GetOsobaByPhoneNumber_ZwracaPoprawnaOsobe()
        {
            var context = GetInMemoryDbContext(nameof(GetOsobaByPhoneNumber_ZwracaPoprawnaOsobe));
            var osoba = new Pacjent { Id = 1, Login = "login1", Email = "email@example.com", PhoneNumber = "123456789", Rola = Rola.Pacjent };
            context.Osoby.Add(osoba);
            context.SaveChanges();

            var repo = new OsobaRepository(context);
            var result = repo.GetOsobaByPhoneNumber("123456789");

            Assert.NotNull(result);
            Assert.Equal("123456789", result.PhoneNumber);
        }

        [Fact]
        public void UpdateOsoba_AktualizujePoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UpdateOsoba_AktualizujePoprawnie));
            var osoba = new Pacjent { Id = 1, Imie = "Jan", Login = "login1", Email = "email@example.com", PhoneNumber = "111", Rola = Rola.Pacjent };
            context.Osoby.Add(osoba);
            context.SaveChanges();

            var repo = new OsobaRepository(context);
            osoba.Imie = "Adam";
            repo.Update(osoba);
            repo.save();

            var result = repo.GetOsobaById(1);
            Assert.Equal("Adam", result.Imie);
        }

        [Fact]
        public void PobierzWszystkie_ZwracaWszystkieOsoby()
        {
            var context = GetInMemoryDbContext(nameof(PobierzWszystkie_ZwracaWszystkieOsoby));
            context.Osoby.Add(new Pacjent { Id = 1, Login = "l1", Rola = Rola.Pacjent });
            context.Osoby.Add(new Pacjent { Id = 2, Login = "l2", Rola = Rola.Pacjent });
            context.SaveChanges();

            var repo = new OsobaRepository(context);
            var result = repo.PobierzWszystkie().ToList();

            Assert.Equal(2, result.Count);
        }

    }
}
