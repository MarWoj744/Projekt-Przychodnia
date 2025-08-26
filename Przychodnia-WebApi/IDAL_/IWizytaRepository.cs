using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_
{
    public interface IWizytaRepository
    {
        //Task<bool> CzyLekarzMaZajetyTerminAsync(int lekarzId, DateTime data);
        //Task DodajWizyteAsync(Wizyta wizyta);
        //Task<List<Wizyta>> PobierzWszystkieAsync();
        IQueryable<Wizyta> PobierzWszystkie();
        IQueryable<Wizyta> PobierzWizytyLekarza(int lekarzId, DateTime start, DateTime end);
        IQueryable<Wizyta> PobierzWizytyAnulowane();
        IQueryable<Wizyta> PobierzWizytyAnulowaneLekarza(int lekarzId);

        Wizyta getWizytaById(int id);
        void dodaj(Wizyta wizyta);
        void delete(int id);
        void update(Wizyta wizyta);
        void save();
    }
}
