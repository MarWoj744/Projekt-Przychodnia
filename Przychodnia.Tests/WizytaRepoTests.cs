using Xunit;
using Microsoft.EntityFrameworkCore;

using Przychodnia.Repositories;
using System.Linq;
using Models;

namespace Przychodnia.Tests
{
    public class WizytaRepositoryTests
    {
        private DbPrzychodnia GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DbPrzychodnia>()
                 .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new DbPrzychodnia(options);
        }

        [Fact]
        public void DodajWizyte_DodajePoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(DodajWizyte_DodajePoprawnie));
            var repo = new WizytaRepository(context);
            var wizyta = new Wizyta { Id = 1, Opis = "Kontrola" };

            // Act
            repo.dodaj(wizyta);
            repo.save();

            // Assert
            var result = repo.getWizytaById(1);
            Assert.NotNull(result);
            Assert.Equal("Kontrola", result.Opis);
        }

        [Fact]
        public void UsunWizyte_UsuwaPoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UsunWizyte_UsuwaPoprawnie));
            var wizyta = new Wizyta { Id = 2, Opis = "Do usunięcia" };
            context.Wizyty.Add(wizyta);
            context.SaveChanges();

            var repo = new WizytaRepository(context);

            // Act
            repo.delete(2);
            repo.save();

            // Assert
            var result = repo.getWizytaById(2);
            Assert.Null(result);
        }

        [Fact]
        public void PobierzWszystkie_ZwracaWszystkieWizyty()
        {
            var context = GetInMemoryDbContext(nameof(PobierzWszystkie_ZwracaWszystkieWizyty));
            context.Wizyty.Add(new Wizyta { Id = 5, Opis = "W1" });
            context.Wizyty.Add(new Wizyta { Id = 6, Opis = "W2" });
            context.SaveChanges();

            var repo = new WizytaRepository(context);

            // Act
            var result = repo.PobierzWszystkie().ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, w => w.Opis == "W1");
            Assert.Contains(result, w => w.Opis == "W2");
        }

        [Fact]
        public void GetWizytaById_ZwracaPoprawnaWizyte()
        {
            var context = GetInMemoryDbContext(nameof(GetWizytaById_ZwracaPoprawnaWizyte));
            var wizyta = new Wizyta { Id = 4, Opis = "Wizyta testowa" };
            context.Wizyty.Add(wizyta);
            context.SaveChanges();

            var repo = new WizytaRepository(context);

            // Act
            var result = repo.getWizytaById(4);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Wizyta testowa", result.Opis);
        }

        [Fact]
        public void UpdateWizyta_AktualizujePoprawnie()
        {
            var context = GetInMemoryDbContext(nameof(UpdateWizyta_AktualizujePoprawnie));
            var wizyta = new Wizyta { Id = 3, Opis = "Stary opis" };
            context.Wizyty.Add(wizyta);
            context.SaveChanges();

            var repo = new WizytaRepository(context);

            // Act
            wizyta.Opis = "Nowy opis";
            repo.update(wizyta);
            repo.save();

            var result = repo.getWizytaById(3);

            // Assert
            Assert.Equal("Nowy opis", result.Opis);
        }

    }
}
