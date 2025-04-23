using Przychodnia.DTOs;

namespace Przychodnia.Services
{
    public interface IWykonaneBadanieService
    {
        Task DodajWykonaneBadanieAsync(WykonaneBadaniaDTO dto);
    }
}
