using IDAL_;
using Models;
//using Przychodnia.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTests
{
    class MockWizytaRepository:IWizytaRepository
    {
        public bool AddCalled { get; private set; } = false;
        public bool SaveCalled { get; private set; } = false;
        public void delete(int id)
        {
          
        }

        public void dodaj(Wizyta wizyta)
        {
            AddCalled = true;
        }

        public Wizyta getWizytaById(int id)
        {
            return null;
        }

        public IQueryable<Wizyta> PobierzWszystkie()
        {
            return null;
        }

        public void save()
        {
            SaveCalled = true;
        }

        public void update(Wizyta wizyta)
        {
            
        }
    }
}
