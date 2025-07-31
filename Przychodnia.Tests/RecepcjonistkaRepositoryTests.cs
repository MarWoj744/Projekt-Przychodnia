using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Models;
using DAL;

namespace Przychodnia.Tests
{
    public class RecepcjonistkaRepositoryTests
    {
        private DbPrzychodnia GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DbPrzychodnia>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new DbPrzychodnia(options);
        }

        [Fact]
        public void DodajRecepcjonistke_DzialaPoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(DodajRecepcjonistke_DzialaPoprawnie));
            var repo = new RecepcjonistkaRepository(context);

            var recepcjonistka = new Recepcjonistka
            {
                Id = 1,
                Imie = "Agnieszka",
                Nazwisko = "Nowicka"
            };

            repo.Dodaj(recepcjonistka);
            repo.save();

            var result = repo.GetRecepcjonistkaById(1);

            Assert.NotNull(result);
            Assert.Equal("Agnieszka", result.Imie);
        }

        [Fact]
        public void UsunRecepcjonistke_UsuwaPoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UsunRecepcjonistke_UsuwaPoprawnie));
            var recepcjonistka = new Recepcjonistka
            {
                Id = 2,
                Imie = "Katarzyna",
                Nazwisko = "Lis"
            };

            context.Recepcjonistki.Add(recepcjonistka);
            context.SaveChanges();

            var repo = new RecepcjonistkaRepository(context);
            repo.Delete(2);
            repo.save();

            var result = repo.GetRecepcjonistkaById(2);
            Assert.Null(result);
        }

        [Fact]
        public void UpdateRecepcjonistke_AktualizujePoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UpdateRecepcjonistke_AktualizujePoprawnie));
            var recepcjonistka = new Recepcjonistka
            {
                Id = 3,
                Imie = "Monika",
                Nazwisko = "Kowalska"
            };

            context.Recepcjonistki.Add(recepcjonistka);
            context.SaveChanges();

            var repo = new RecepcjonistkaRepository(context);
            recepcjonistka.Nazwisko = "Nowak";
            repo.Update(recepcjonistka);
            repo.save();

            var result = repo.GetRecepcjonistkaById(3);

            Assert.Equal("Nowak", result.Nazwisko);
        }

        [Fact]
        public void GetRecepcjonistkaById_ZwracaPoprawna()
        {
            var context = GetInMemoryDbContext(nameof(GetRecepcjonistkaById_ZwracaPoprawna));
            var recepcjonistka = new Recepcjonistka
            {
                Id = 4,
                Imie = "Beata",
                Nazwisko = "Zając"
            };

            context.Recepcjonistki.Add(recepcjonistka);
            context.SaveChanges();

            var repo = new RecepcjonistkaRepository(context);
            var result = repo.GetRecepcjonistkaById(4);

            Assert.NotNull(result);
            Assert.Equal("Beata", result.Imie);
        }

        [Fact]
        public void PobierzWszystkie_ZwracaWszystkieRecepcjonistki()
        {
            var context = GetInMemoryDbContext(nameof(PobierzWszystkie_ZwracaWszystkieRecepcjonistki));
            context.Recepcjonistki.Add(new Recepcjonistka { Id = 1, Imie = "Anna", Nazwisko = "Lewandowska" });
            context.Recepcjonistki.Add(new Recepcjonistka { Id = 2, Imie = "Magda", Nazwisko = "Kruk" });
            context.SaveChanges();

            var repo = new RecepcjonistkaRepository(context);
            var result = repo.PobierzWszystkie().ToList();

            Assert.Equal(2, result.Count);
        }
    }
}

