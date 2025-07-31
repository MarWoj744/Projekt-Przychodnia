using IDAL_;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTests
{
    internal class DummyLekarzRepository : ILekarzRepository
    {
        public void Delete(int id)
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }

        public void Dodaj(Lekarz lekarz)
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }

        public Lekarz GetLekarzById(int id)
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }

        public IQueryable<Lekarz> PobierzWszystkie()
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }

        public void save()
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }

        public void Update(Lekarz lekarz)
        {
            throw new NotImplementedException("ta implementacja repo nie powinna być użyta");
        }
    }
}
