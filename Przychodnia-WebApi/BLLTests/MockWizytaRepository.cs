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
        private List<Wizyta> _wizyty = new List<Wizyta>();
        public bool AddCalled { get; private set; } = false;
        public bool SaveCalled { get; private set; } = false;
        public bool DeleteCalled { get; private set; } = false;
        public void delete(int id)
        {
          DeleteCalled = true;
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

        public IQueryable<Wizyta> PobierzWizytyLekarza(int lekarzId, DateTime start, DateTime end)
        {
            return _wizyty
                .Where(w => w.LekarzId == lekarzId && w.Data >= start && w.Data <= end)
                .AsQueryable();
        }
        public IQueryable<Wizyta> GetWizytyPacjenta(int pacjentId)
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

        public IQueryable<Wizyta> PobierzWizytyAnulowane()
        {
            return null;
        }

        public IQueryable<Wizyta> PobierzWizytyAnulowaneLekarza(int lekarzId)
        {
            return null;
        }
    }
}
