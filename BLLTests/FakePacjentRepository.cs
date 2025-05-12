using IBLL;
using IDAL_;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLLTests
{
    class FakePacjentRepository : IPacjentRepository
    {
        private readonly List<Pacjent> _data = new();

       

        public Pacjent GetPacjentById(int id)
        {
           return _data.FirstOrDefault(e => e.Id == id);
        }

        public void Dodaj(Pacjent pacjent)
        {
            _data.Add(pacjent);
        }

        public void Delete(int id)
        {
            var pacjent = GetPacjentById(id);
            if (pacjent != null)
            {
                _data.Remove(pacjent);
            }
        }

        public void Update(Pacjent pacjent)
        {
            var index = _data.FindIndex(e => e.Id == pacjent.Id);
            if (index != -1)
            {
                _data[index] = pacjent;
            }
        }

        public IQueryable<Pacjent> PobierzWszystkie()
        {
            return _data.AsQueryable(); ;
        }

        public void save()
        {
            
        }
    }
}
