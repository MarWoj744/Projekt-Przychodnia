using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.DTOs;
using Przychodnia.Models;

namespace Przychodnia.Services
{
    public class WykonaneBadaniaService : IWykonaneBadanieService
    {
        private readonly DbPrzychodnia _context;

        public WykonaneBadaniaService(DbPrzychodnia context)
        {
            _context = context;
        }

        public async Task DodajWykonaneBadanieAsync(WykonaneBadaniaDTO dto)
        {
            //sprawdzanie czy wizyta oraz badanie istnieja
            var wizyta = await _context.Wizyty.FindAsync(dto.WizytaId);
            if (wizyta == null)
            {
                throw new Exception("Wizyta nie istnieje.");
            }

            var badanie = await _context.Badania.FindAsync(dto.BadanieId);
            if (badanie == null)
            {
                throw new Exception("Badanie nie istnieje.");
            }

            //specjalizacja lekarza
            if (wizyta.Lekarz.Specjalizacja != badanie.Specjalizacja)
            {
                throw new Exception("Specjalizacja lekarza nie odpowiada badaniu.");
            }

            var wykonane = new WykonaneBadania
            {
                WizytaId = dto.WizytaId,
                BadanieId = dto.BadanieId,
                Data = dto.Data,
                Wyniki = dto.Wyniki
            };

            _context.WykonaneBadania.Add(wykonane);
            await _context.SaveChangesAsync();
        }
    }
}