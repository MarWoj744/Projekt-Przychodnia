using IDAL_;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTests
{
    class DummyPacjentRepository : IPacjentRepository
    {
        public void Delete(int id)
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }

        public void Dodaj(Pacjent pacjent)
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }

        public Pacjent GetPacjentById(int id)
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }

        public IQueryable<Pacjent> PobierzWszystkie()
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }

        public void save()
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }

        public void Update(Pacjent pacjent)
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }
    }
}
