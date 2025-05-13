using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Models;
using DAL;

namespace Przychodnia.Tests
{
    public class LekarzRepositoryTests
    {
        private DbPrzychodnia GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DbPrzychodnia>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new DbPrzychodnia(options);
        }

        [Fact]
        public void DodajLekarza_DzialaPoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(DodajLekarza_DzialaPoprawnie));
            var repo = new LekarzRepository(context);

            var lekarz = new Lekarz
            {
                Id = 1,
                Imie = "Anna",
                Nazwisko = "Nowak",
                Tytul = "dr",
                Specjalizacja = "Kardiologia"
            };

            repo.Dodaj(lekarz);
            repo.save();

            var result = repo.GetLekarzById(1);

            Assert.NotNull(result);
            Assert.Equal("Anna", result.Imie);
        }

        [Fact]
        public void UsunLekarza_UsuwaPoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UsunLekarza_UsuwaPoprawnie));
            var lekarz = new Lekarz
            {
                Id = 1,
                Imie = "Tomasz",
                Nazwisko = "Kowalski",
                Tytul = "lek.",
                Specjalizacja = "Dermatologia"
            };

            context.Lekarze.Add(lekarz);
            context.SaveChanges();

            var repo = new LekarzRepository(context);
            repo.Delete(1);
            repo.save();

            var result = repo.GetLekarzById(1);

            Assert.Null(result);
        }

        [Fact]
        public void UpdateLekarza_AktualizujePoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UpdateLekarza_AktualizujePoprawnie));
            var lekarz = new Lekarz
            {
                Id = 1,
                Imie = "Piotr",
                Nazwisko = "Zieliński",
                Tytul = "dr",
                Specjalizacja = "Ortopedia"
            };

            context.Lekarze.Add(lekarz);
            context.SaveChanges();

            var repo = new LekarzRepository(context);
            lekarz.Specjalizacja = "Neurologia";
            repo.Update(lekarz);
            repo.save();

            var result = repo.GetLekarzById(1);

            Assert.Equal("Neurologia", result.Specjalizacja);
        }

        [Fact]
        public void GetLekarzById_ZwracaPoprawnegoLekarza()
        {
            var context = GetInMemoryDbContext(nameof(GetLekarzById_ZwracaPoprawnegoLekarza));
            var lekarz = new Lekarz
            {
                Id = 1,
                Imie = "Elżbieta",
                Nazwisko = "Mazur",
                Tytul = "prof.",
                Specjalizacja = "Ginekologia"
            };

            context.Lekarze.Add(lekarz);
            context.SaveChanges();

            var repo = new LekarzRepository(context);
            var result = repo.GetLekarzById(1);

            Assert.NotNull(result);
            Assert.Equal("Ginekologia", result.Specjalizacja);
        }

        [Fact]
        public void PobierzWszystkie_ZwracaWszystkichLekarzy()
        {
            var context = GetInMemoryDbContext(nameof(PobierzWszystkie_ZwracaWszystkichLekarzy));
            context.Lekarze.Add(new Lekarz { Id = 1, Imie = "Jan", Nazwisko = "Kowal", Tytul = "dr", Specjalizacja = "Urologia" });
            context.Lekarze.Add(new Lekarz { Id = 2, Imie = "Ewa", Nazwisko = "Nowicka", Tytul = "lek.", Specjalizacja = "Pediatria" });
            context.SaveChanges();

            var repo = new LekarzRepository(context);
            var result = repo.PobierzWszystkie().ToList();

            Assert.Equal(2, result.Count);
        }
    }
}