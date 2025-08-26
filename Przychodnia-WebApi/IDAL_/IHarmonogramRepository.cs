using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_
{
    public interface IHarmonogramRepository
    {
        IQueryable<Harmonogram> GetAll();
        IQueryable<Harmonogram> GetByLekarzId(int lekarzId);
        Harmonogram GetById(int id);
        void Dodaj(Harmonogram harmonogram);
        void Update(Harmonogram harmonogram);
        void Delete(int id);
        void Save();
    }
}
