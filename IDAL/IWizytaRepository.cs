using Przychodnia.Models;

namespace Przychodnia.Repositories
{
    public interface IWizytaRepository
    {
        //Task<bool> CzyLekarzMaZajetyTerminAsync(int lekarzId, DateTime data);
        //Task DodajWizyteAsync(Wizyta wizyta);
        //Task<List<Wizyta>> PobierzWszystkieAsync();
        IQueryable<Wizyta> PobierzWszystkie();
        Wizyta getWizytaById(int id);
        void dodaj(Wizyta wizyta);
        void delete(int id);
        void update(Wizyta wizyta);
        void save();
    }
}