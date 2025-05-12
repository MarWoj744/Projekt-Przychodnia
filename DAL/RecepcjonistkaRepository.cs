using IDAL_;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RecepcjonistkaRepository : IRecepcjonistkaRepository
    {
        private readonly DbPrzychodnia _context;
        public RecepcjonistkaRepository(DbPrzychodnia context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            var recepc = GetRecepcjonistkaById(id);
            if (recepc != null)
            {
                _context.Recepcjonistki.Remove(recepc);
            }
        }

        public void Dodaj(Recepcjonistka recepcjonistka)
        {
            _context.Recepcjonistki.Add(recepcjonistka);
        }

        public Recepcjonistka GetRecepcjonistkaById(int id)
        {
            return _context.Recepcjonistki.FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<Recepcjonistka> PobierzWszystkie()
        {
            return _context.Recepcjonistki;
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void Update(Recepcjonistka recepcjonistka)
        {
            _context.Recepcjonistki.Update(recepcjonistka);
        }
    }
}
