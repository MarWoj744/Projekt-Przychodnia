using DTOs;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IWizytaService
    {
        Task<bool> ZarejestrujWizyteAsync(RejestracjaWizytyDTO dto);
        IQueryable<Wizyta> GetAll();
        Wizyta GetWizytaById(int id);
        Task<bool> UpdateWizytaAsync(Wizyta wizyta);
        Task<bool> DeleteWizytaAsync(int id);
    }
}
