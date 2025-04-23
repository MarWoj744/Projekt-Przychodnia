using Przychodnia.Models;

namespace Przychodnia.Services
{
    public interface IPacjentService
    {
        string ValidatePesel(Pacjent pacjent);
    }
}