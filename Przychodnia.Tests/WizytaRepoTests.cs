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
    }
}
