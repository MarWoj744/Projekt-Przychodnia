using DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ILekarzService
    {
        Lekarz GetLekarzById(int id);
        IQueryable<Lekarz> PobierzWszystkie();
        void Dodaj(Lekarz lekarz);
        void Delete(int id);
        void Update(Lekarz lekarz);
        void save();
        IQueryable<Wizyta> PobierzHarmonogramLekarza(int lekarzId);
        IQueryable<Wizyta> PobierzAnulowaneWizytyLekarza(int lekarzId);
        void EdytujBadanie(Badanie badanie);
    }
}
