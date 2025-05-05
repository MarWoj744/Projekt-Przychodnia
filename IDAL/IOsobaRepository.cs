using Przychodnia.Models;

namespace Przychodnia.Repositories
{
    public interface IOsobaRepository
    {
        IQueryable<Osoba> PobierzWszystkie();
        Osoba GetOsobaById(int id);
        Osoba GetOsobaByLogin(string login);
        Osoba GetOsobaByEmail(string email);
        Osoba GetOsobaByPhoneNumber(string phoneNumber);
        void Dodaj(Osoba osoba);
        void Delete(int id);
        void Update(Osoba osoba);
        void save();
    }
}