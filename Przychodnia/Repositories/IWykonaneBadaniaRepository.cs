using Przychodnia.Models;

namespace Przychodnia.Repositories
{
    public interface IWykonaneBadaniaRepository
    {
        Task<Wizyta?> GetWizytaByIdAsync(int id);
        Task<Badanie?> GetBadanieByIdAsync(int id);
        Task DodajAsync(WykonaneBadania wykonaneBadania);
    }
}