using IDAL_;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LekarzRepository : ILekarzRepository
    {
        private readonly DbPrzychodnia _context;
        public LekarzRepository(DbPrzychodnia context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            var lek = GetLekarzById(id);
            if (lek != null)
            {
                _context.Lekarze.Remove(lek);
            }
            ;
        }

        public void Dodaj(Lekarz lekarz)
        {
            _context.Lekarze.Add(lekarz);
        }

        public Lekarz GetLekarzById(int id)
        {
            return _context.Lekarze.FirstOrDefault(l => l.Id == id);
        }

        public IQueryable<Lekarz> PobierzWszystkie()
        {
            return _context.Lekarze;
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void Update(Lekarz lekarz)
        {
            _context.Lekarze.Update(lekarz);
        }
        public IQueryable<Wizyta> GetHarmonogramLekarza(int lekarzId)
        {
            return _context.Wizyty.Where(w => w.LekarzId == lekarzId && w.Status != "Anulowana");
        }

        public IQueryable<Wizyta> GetAnulowaneWizytyLekarza(int lekarzId)
        {
            return _context.Wizyty.Where(w => w.LekarzId == lekarzId && w.Status == "Anulowana");
        }

        public void UpdateBadanie(Badanie badanie)
        {
            _context.Badania.Update(badanie);
        }
    }
}
