using IDAL_;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTests
{
    internal class FakeRecepcjonistkaRepository : IRecepcjonistkaRepository
    {
        private readonly List<Recepcjonistka> _data = new();
        public void Delete(int id)
        {
            var recepcjonistka = GetRecepcjonistkaById(id);
            if (recepcjonistka != null)
            {
                _data.Remove(recepcjonistka);
            }
        }

        public void Dodaj(Recepcjonistka recepcjonistka)
        {
            _data.Add(recepcjonistka);
        }

        public Recepcjonistka GetRecepcjonistkaById(int id)
        {
            return _data.FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<Recepcjonistka> PobierzWszystkie()
        {
            return _data.AsQueryable();
        }

        public void save()
        {
            
        }

        public void Update(Recepcjonistka recepcjonistka)
        {
            var index = _data.FindIndex(e => e.Id == recepcjonistka.Id);
            if (index != -1) _data[index] = recepcjonistka;
        }
    }
}
