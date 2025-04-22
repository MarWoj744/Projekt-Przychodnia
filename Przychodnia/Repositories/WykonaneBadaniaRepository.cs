using Microsoft.EntityFrameworkCore;
using Przychodnia.Models;
namespace Przychodnia.Repositories
{
    public class WykonaneBadaniaRepository : IWykonaneBadaniaRepository
    {
        private readonly DbPrzychodnia _context;
 public WykonaneBadaniaRepository(DbPrzychodnia context)
        {
            _context = context; 
        }
        public async Task<Wizyta?> GetWizytaByIdAsync(int id)
        {
            return await _context.Wizyty
                .Include(w => w.Lekarz)
                .FirstOrDefaultAsync(w => w.Id == id);}
public async Task<Badanie?> GetBadanieByIdAsync(int id)
        {
            return await _context.Badania.FindAsync(id);
        }
        public async Task DodajAsync(WykonaneBadania wykonaneBadania)
        {
            _context.WykonaneBadania.Add(wykonaneBadania);
            await _context.SaveChangesAsync();
        }
    }
}
