using Przychodnia.Models;

namespace Przychodnia.Repositories
{
    public interface IOsobaRepository
    {
        Osoba GetOsobaByLogin(string login);
        Osoba GetOsobaByEmail(string email);
        Osoba GetOsobaByPhoneNumber(string phoneNumber);
    }
}