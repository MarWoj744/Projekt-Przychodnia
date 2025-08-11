using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IHarmonogramService
    {
        IQueryable<Harmonogram> PobierzWszystkie();
        IQueryable<Harmonogram> PobierzPoLekarzId(int lekarzId);
        Harmonogram GetHarmonogramById(int id);
        void Dodaj(Harmonogram harmonogram);
        void Update(Harmonogram harmonogram);
        void Delete(int id);
        void Save();

      
        bool CzyTerminDostepny(int lekarzId, DateTime dataOd, DateTime dataDo);
    }
}
