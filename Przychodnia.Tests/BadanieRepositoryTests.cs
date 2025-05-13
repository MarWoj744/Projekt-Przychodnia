using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Models;
using DAL;

namespace Przychodnia.Tests
{
    public class BadanieRepositoryTests
    {
        private DbPrzychodnia GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DbPrzychodnia>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new DbPrzychodnia(options);
        }

        [Fact]
        public void DodajBadanie_DzialaPoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(DodajBadanie_DzialaPoprawnie));
            var repo = new BadanieRepository(context);

            var badanie = new Badanie
            {
                Id = 1,
                Nazwa = "Morfologia",
                Cennik = 50.0m,
                Specjalizacja = "Diagnostyka"
            };

            repo.Dodaj(badanie);
            repo.save();

            var result = repo.GetBadanieById(1);

            Assert.NotNull(result);
            Assert.Equal("Morfologia", result.Nazwa);
        }

        [Fact]
        public void UsunBadanie_UsuwaPoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UsunBadanie_UsuwaPoprawnie));
            var badanie = new Badanie
            {
                Id = 1,
                Nazwa = "RTG",
                Cennik = 120.0m,
                Specjalizacja = "Radiologia"
            };

            context.Badania.Add(badanie);
            context.SaveChanges();

            var repo = new BadanieRepository(context);
            repo.Delete(1);
            repo.save();

            var result = repo.GetBadanieById(1);

            Assert.Null(result);
        }

        [Fact]
        public void UpdateBadanie_AktualizujePoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UpdateBadanie_AktualizujePoprawnie));
            var badanie = new Badanie
            {
                Id = 1,
                Nazwa = "USG",
                Cennik = 200.0m,
                Specjalizacja = "Diagnostyka"
            };

            context.Badania.Add(badanie);
            context.SaveChanges();

            var repo = new BadanieRepository(context);
            badanie.Cennik = 220.0m;
            repo.Update(badanie);
            repo.save();

            var result = repo.GetBadanieById(1);

            Assert.Equal(220.0m, result.Cennik);
        }

        [Fact]
        public void GetBadanieById_ZwracaPoprawneBadanie()
        {
            var context = GetInMemoryDbContext(nameof(GetBadanieById_ZwracaPoprawneBadanie));
            var badanie = new Badanie
            {
                Id = 1,
                Nazwa = "Echo serca",
                Cennik = 180.0m,
                Specjalizacja = "Kardiologia"
            };

            context.Badania.Add(badanie);
            context.SaveChanges();

            var repo = new BadanieRepository(context);
            var result = repo.GetBadanieById(1);

            Assert.NotNull(result);
            Assert.Equal("Echo serca", result.Nazwa);
        }

        [Fact]
        public void PobierzWszystkie_ZwracaWszystkieBadania()
        {
            var context = GetInMemoryDbContext(nameof(PobierzWszystkie_ZwracaWszystkieBadania));
            context.Badania.Add(new Badanie { Id = 1, Nazwa = "RTG", Cennik = 100, Specjalizacja = "Radiologia" });
            context.Badania.Add(new Badanie { Id = 2, Nazwa = "EKG", Cennik = 70, Specjalizacja = "Kardiologia" });
            context.SaveChanges();

            var repo = new BadanieRepository(context);
            var result = repo.PobierzWszystkie().ToList();

            Assert.Equal(2, result.Count);
        }
    }
}
