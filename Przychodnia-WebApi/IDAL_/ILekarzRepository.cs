using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_
{
    public interface ILekarzRepository
    {
        Lekarz GetLekarzById(int id);
        IQueryable<Lekarz> PobierzWszystkie();
        IQueryable<Wizyta> GetHarmonogramLekarza(int lekarzId);
        IQueryable<Wizyta> GetAnulowaneWizytyLekarza(int lekarzId);
        void UpdateBadanie(Badanie badanie);
      

        void Dodaj(Lekarz lekarz);
        void Delete(int id);
        void Update(Lekarz lekarz);
        void save();
    }
}
