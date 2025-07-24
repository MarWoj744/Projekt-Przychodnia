using IDAL_;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PacjentRepository : IPacjentRepository
    {
        private readonly DbPrzychodnia _context;
        public PacjentRepository(DbPrzychodnia context)
        {
            _context = context;
        }


        public void Delete(int id)
        {
            var pacjent = GetPacjentById(id);
            if (pacjent != null)
            {
                _context.Pacjenci.Remove(pacjent);
            }
        }

        public void Dodaj(Pacjent pacjent)
        {
            _context.Pacjenci.Add(pacjent);
        }

        public Pacjent GetPacjentById(int id)
        {
            return _context.Pacjenci.FirstOrDefault(p => p.Id == id);
        }


        public IQueryable<Pacjent> PobierzWszystkie()
        {
            return _context.Pacjenci;
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void Update(Pacjent pacjent)
        {
            _context.Pacjenci.Update(pacjent);
        }
    }
}
