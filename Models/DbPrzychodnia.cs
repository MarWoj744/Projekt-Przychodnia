using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class DbPrzychodnia : DbContext
    {

        public DbPrzychodnia(DbContextOptions<DbPrzychodnia> options) : base(options)
        {
        }
        public DbSet<Osoba> Osoby { get; set; }
        public DbSet<Pacjent> Pacjenci { get; set; }
        public DbSet<Lekarz> Lekarze { get; set; }
        public DbSet<Recepcjonistka> Recepcjonistki { get; set; }
        public DbSet<Badanie> Badania { get; set; }
        public DbSet<Wizyta> Wizyty { get; set; }
        public DbSet<WykonaneBadania> WykonaneBadania { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Osoba>()
                .HasDiscriminator<Rola>("Rola")
                .HasValue<Pacjent>(Rola.Pacjent)
                .HasValue<Lekarz>(Rola.Lekarz)
                .HasValue<Recepcjonistka>(Rola.Recepcjonistka);

            modelBuilder.Entity<Pacjent>()
                .HasIndex(p => p.PESEL)
                .IsUnique();

            modelBuilder.Entity<Wizyta>()
                .HasOne(w => w.Pacjent)
                .WithMany(p => p.Wizyty)
                .HasForeignKey(w => w.PacjentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wizyta>()
                .HasOne(w => w.Lekarz)
                .WithMany(l => l.Wizyty)
                .HasForeignKey(w => w.LekarzId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wizyta>()
                .HasOne(w => w.Recepcjonistka)
                .WithMany(r => r.WizytyZarejestrowane)
                .HasForeignKey(w => w.RecepcjonistkaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WykonaneBadania>()
                .HasOne(wb => wb.Wizyta)
                .WithMany(w => w.Badania)
                .HasForeignKey(wb => wb.WizytaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WykonaneBadania>()
                .HasOne(wb => wb.Badanie)
                .WithMany(b => b.Wykonane)
                .HasForeignKey(wb => wb.BadanieId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Badanie>()
                .Property(b => b.Cennik)
                .HasPrecision(18, 2);
        }
    }
}
