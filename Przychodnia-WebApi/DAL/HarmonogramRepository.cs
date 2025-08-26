using IDAL_;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HarmonogramRepository : IHarmonogramRepository
    {
        private readonly DbPrzychodnia _context;

        public HarmonogramRepository(DbPrzychodnia context)
        {
            _context = context;
        }

        public IQueryable<Harmonogram> GetAll()
        {
            return _context.Harmonogramy.AsNoTracking();
        }

        public IQueryable<Harmonogram> GetByLekarzId(int lekarzId)
        {
            return _context.Harmonogramy.Where(h => h.LekarzId == lekarzId).AsNoTracking();
        }

        public Harmonogram GetById(int id)
        {
            return _context.Harmonogramy.Find(id);
        }

        public void Dodaj(Harmonogram harmonogram)
        {
            _context.Harmonogramy.Add(harmonogram);
        }

        public void Update(Harmonogram harmonogram)
        {
            _context.Harmonogramy.Update(harmonogram);
        }

        public void Delete(int id)
        {
            var h = _context.Harmonogramy.Find(id);
            if (h != null)
            {
                _context.Harmonogramy.Remove(h);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
