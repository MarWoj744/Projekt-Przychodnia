using Przychodnia.DTOs;
using Przychodnia.Models;

namespace Przychodnia.Services
{
    public interface IWizytaService
    {
        Task<bool> ZarejestrujWizyteAsync(RejestracjaWizytyDTO dto);
        Task<List<Wizyta>> GetAllAsync();
    }
}