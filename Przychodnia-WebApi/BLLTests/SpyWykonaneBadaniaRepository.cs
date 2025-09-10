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
    class SpyWykonaneBadaniaRepository : IWykonaneBadaniaRepository
    {
        public List<WykonaneBadania> AddedEntities { get; } = new();
        public bool SaveCalled { get; private set; } = false;

        public void delete(int id)
        {
            var entity = GetWykonaneBadaniaById(id);
            if (entity != null) AddedEntities.Remove(entity);
        }

        public void dodaj(WykonaneBadania badania)
        {
            AddedEntities.Add(badania);
        }

        public IQueryable<WykonaneBadania> GetAll()
        {
            return AddedEntities.AsQueryable(); ;
        }
        public IQueryable<WykonaneBadania> getByPacjentId(int pacjentId)
        {
            return AddedEntities.AsQueryable(); ;
        }
        public WykonaneBadania GetWykonaneBadaniaById(int id)
        {
            return AddedEntities.FirstOrDefault(e => e.Id == id);
        }

        public void save()
        {
            SaveCalled = true;
            
        }

        public void update(WykonaneBadania badania)
        {
            var index = AddedEntities.FindIndex(e => e.Id == badania.Id);
            if (index != -1) AddedEntities[index] = badania;
        }
    }
}
