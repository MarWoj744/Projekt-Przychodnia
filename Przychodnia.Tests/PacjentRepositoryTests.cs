using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Models;
using DAL;

namespace Przychodnia.Tests
{
    public class PacjentRepositoryTests
    {
        private DbPrzychodnia GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DbPrzychodnia>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new DbPrzychodnia(options);
        }

        [Fact]
        public void DodajPacjenta_DzialaPoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(DodajPacjenta_DzialaPoprawnie));
            var repo = new PacjentRepository(context);

            var pacjent = new Pacjent
            {
                Id = 1,
                Imie = "Marek",
                Nazwisko = "Nowak",
                PESEL = "85010112345"
            };

            repo.Dodaj(pacjent);
            repo.save();

            var result = repo.GetPacjentById(1);

            Assert.NotNull(result);
            Assert.Equal("Marek", result.Imie);
        }

        [Fact]
        public void UsunPacjenta_UsuwaPoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UsunPacjenta_UsuwaPoprawnie));
            var pacjent = new Pacjent
            {
                Id = 1,
                Imie = "Anna",
                Nazwisko = "Zielińska",
                PESEL = "90010112345"
            };

            context.Pacjenci.Add(pacjent);
            context.SaveChanges();

            var repo = new PacjentRepository(context);
            repo.Delete(1);
            repo.save();

            var result = repo.GetPacjentById(1);
            Assert.Null(result);
        }

        [Fact]
        public void UpdatePacjenta_AktualizujePoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UpdatePacjenta_AktualizujePoprawnie));
            var pacjent = new Pacjent
            {
                Id = 1,
                Imie = "Katarzyna",
                Nazwisko = "Mazur",
                PESEL = "91010112345"
            };

            context.Pacjenci.Add(pacjent);
            context.SaveChanges();

            var repo = new PacjentRepository(context);
            pacjent.Nazwisko = "Nowicka";
            repo.Update(pacjent);
            repo.save();

            var result = repo.GetPacjentById(1);

            Assert.Equal("Nowicka", result.Nazwisko);
        }

        [Fact]
        public void GetPacjentById_ZwracaPoprawnegoPacjenta()
        {
            var context = GetInMemoryDbContext(nameof(GetPacjentById_ZwracaPoprawnegoPacjenta));
            var pacjent = new Pacjent
            {
                Id = 1,
                Imie = "Tomasz",
                Nazwisko = "Kowal",
                PESEL = "82010112345"
            };

            context.Pacjenci.Add(pacjent);
            context.SaveChanges();

            var repo = new PacjentRepository(context);
            var result = repo.GetPacjentById(1);

            Assert.NotNull(result);
            Assert.Equal("82010112345", result.PESEL);
        }

        [Fact]
        public void PobierzWszystkie_ZwracaWszystkichPacjentow()
        {
            var context = GetInMemoryDbContext(nameof(PobierzWszystkie_ZwracaWszystkichPacjentow));
            context.Pacjenci.Add(new Pacjent { Id = 1, Imie = "Adam", Nazwisko = "Lis", PESEL = "85010111111" });
            context.Pacjenci.Add(new Pacjent { Id = 2, Imie = "Ewa", Nazwisko = "Wilk", PESEL = "87010122222" });
            context.SaveChanges();

            var repo = new PacjentRepository(context);
            var result = repo.PobierzWszystkie().ToList();

            Assert.Equal(2, result.Count);
        }
    }
}