using Models;

namespace IBLL
{
    public interface IOsobaService
    {
        string ValidateData(Osoba osoba);
        bool IsValidEmail(string email);
        bool IsValidPhoneNumber(string phoneNumber);
        IQueryable<Osoba> PobierzWszystkie();
        Osoba GetOsobaById(int id);
        Osoba GetOsobaByLogin(string login);
        Osoba GetOsobaByEmail(string email);
        Osoba GetOsobaByPhoneNumber(string phoneNumber);
        void Dodaj(Osoba osoba);
        void Delete(int id);
        void Update(Osoba osoba);
        void Save();
    }
}
