using Microsoft.EntityFrameworkCore;
using Przychodnia.Models;

namespace Przychodnia.Models
{
    public class DbPrzychodnia : DbContext
    { 
        public DbSet<Badanie> Badania { get; set; }
        public DbSet<Lekarz> Lekarze { get; set; }
        public DbSet<Pacjent> Pacjenci { get; set; }
        public DbSet<Recepcjonistka> Recepcjonistki { get; set; }
        public DbSet<Wizyta> Wizyty { get; set; }
        public DbSet<WykonaneBadania> WykonaneBadania { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Przychodnia;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wizyta>()
                .HasOne(w => w.Pacjent)
                .WithMany(p => p.Wizyty)
                .HasForeignKey(w => w.PacjentId);

            modelBuilder.Entity<Wizyta>()
                .HasOne(w => w.Lekarz)
                .WithMany(l => l.Wizyty)
                .HasForeignKey(w => w.LekarzId);

            modelBuilder.Entity<Wizyta>()
                .HasOne(w => w.Recepcjonistka)
                .WithMany(r => r.WizytyZarejestrowane)
                .HasForeignKey(w => w.RecepcjonistkaId);

            modelBuilder.Entity<WykonaneBadania>()
                .HasOne(wb => wb.Wizyta)
                .WithMany(w => w.Badania)
                .HasForeignKey(wb => wb.WizytaId);

            modelBuilder.Entity<WykonaneBadania>()
                .HasOne(wb => wb.Badanie)
                .WithMany(b => b.Wykonane)
                .HasForeignKey(wb => wb.BadanieId);
        }
    }
}