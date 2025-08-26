using DTOs;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IWizytaService
    {
        Task<bool> ZarejestrujWizyteAsync(Wizyta dto);
        IQueryable<Wizyta> GetAll();
        Wizyta GetWizytaById(int id);
        Task<bool> UpdateWizytaAsync(Wizyta wizyta);
        Task<bool> DeleteWizytaAsync(int id);
        IQueryable<Wizyta> GetWizytyLekarza(int lekarzId, DateTime start, DateTime end);
        IQueryable<Wizyta> GetWizytyAnulowane();
        IQueryable<Wizyta> GetWizytyAnulowaneLekarza(int lekarzId);
        Task<bool> AnulujWizyteAsync(int id);

    }
}
