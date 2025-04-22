using Microsoft.EntityFrameworkCore;
using Przychodnia.Models;
namespace Przychodnia.Repositories
{
    public class WizytaRepository : IWizytaRepository
    {
        private readonly DbPrzychodnia _context;
        public WizytaRepository(DbPrzychodnia context)
        {
            _context = context;
        }
        public async Task<bool> CzyLekarzMaZajetyTerminAsync(int lekarzId, DateTime data)
        {
            return await _context.Wizyty.AnyAsync(w =>
                w.LekarzId == lekarzId && w.Data == data);
        }
        public async Task DodajWizyteAsync(Wizyta wizyta)
        {
            _context.Wizyty.Add(wizyta);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Wizyta>> PobierzWszystkieAsync()
        {
            return await _context.Wizyty
                .Include(w => w.Pacjent)
                .Include(w => w.Lekarz)
                .Include(w => w.Recepcjonistka)
                .ToListAsync();
        }
    }
}
