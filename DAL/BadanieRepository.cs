using IDAL_;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BadanieRepository : IBadanieRepository
    {
        private readonly DbPrzychodnia _context;

        public BadanieRepository(DbPrzychodnia context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var badanie =GetBadanieById(id);
            if (badanie != null)
            {
                _context.Remove(badanie);
            }
        }

        public void Dodaj(Badanie badanie)
        {
            _context.Badania.Add(badanie);
        }

        public Badanie GetBadanieById(int id)
        {
            return _context.Badania.FirstOrDefault(b=>b.Id==id);
        }

        public IQueryable<Badanie> PobierzWszystkie()
        {
            return _context.Badania;
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void Update(Badanie badanie)
        {
            _context.Badania.Update(badanie);
        }


      
    }
}
