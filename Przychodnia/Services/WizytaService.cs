using Przychodnia.DTOs;
using Przychodnia.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Przychodnia.Services
{
    public class WizytaService : IWizytaService
    {
        private readonly DbPrzychodnia _context;

        public WizytaService(DbPrzychodnia context)
        {
            _context = context;
        }

        public async Task<bool> ZarejestrujWizyteAsync(RejestracjaWizytyDTO dto)
        {
            //walidacje
            if (dto.DataWizyty < DateTime.Now)
                throw new Exception("Podane b³êdn¹ datê");

            var lekarzZajety = await _context.Wizyty.AnyAsync(w =>
                w.LekarzId == dto.LekarzId &&
                w.Data == dto.DataWizyty);

            if (lekarzZajety)
                throw new Exception("Termin zajêty.");

            //utworzenie wizyty
            var wizyta = new Wizyta
            {
                PacjentId = dto.PacjentId,
                LekarzId = dto.LekarzId,
                RecepcjonistkaId = dto.RecepcjonistkaId,
                Data = dto.DataWizyty,
                Opis = dto.Opis
            };

            _context.Wizyty.Add(wizyta);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Wizyta>> GetAllAsync()
        {
            return await _context.Wizyty
                .Include(w => w.Pacjent)
                .Include(w => w.Lekarz)
                .Include(w => w.Recepcjonistka)
                .ToListAsync();
        }
    }
}