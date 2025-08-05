using IDAL_;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL
{
    public class WizytaRepository : IWizytaRepository
    {
        private readonly DbPrzychodnia _context;

        public WizytaRepository(DbPrzychodnia context)
        {
            _context = context;
        }

        public IQueryable<Wizyta> PobierzWszystkie()
        {
            return _context.Wizyty;
        }

        public IQueryable<Wizyta> PobierzWizytyLekarza(int lekarzId, DateTime start, DateTime end)
        {
            return _context.Wizyty
                .Where(w => w.LekarzId == lekarzId && w.Data >= start && w.Data <= end);
        }

        public Wizyta getWizytaById(int id)
        {
            return _context.Wizyty.FirstOrDefault(w => w.Id == id);
        }

        public void dodaj(Wizyta wizyta)
        {
            _context.Wizyty.Add(wizyta);
        }

        public void delete(int id)
        {
            var wizyta = getWizytaById(id);
            if (wizyta != null)
            {
                _context.Wizyty.Remove(wizyta);
            }
        }

        public void update(Wizyta wizyta)
        {
            _context.Wizyty.Update(wizyta);
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}