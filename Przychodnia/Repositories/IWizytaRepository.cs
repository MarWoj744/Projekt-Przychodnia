using Przychodnia.Models;

namespace Przychodnia.Repositories
{
    public interface IWizytaRepository
    {
        Task<bool> CzyLekarzMaZajetyTerminAsync(int lekarzId, DateTime data);
        Task DodajWizyteAsync(Wizyta wizyta);
        Task<List<Wizyta>> PobierzWszystkieAsync();
    }
}