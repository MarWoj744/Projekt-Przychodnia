using IBLL;
using Models;

namespace BLLTests
{
    class StubOsobaService : IOsobaService
    {
        private readonly string _validationResult;
        private readonly List<Osoba> _osoby;

        public StubOsobaService(string validationResult, List<Osoba> osoby = null)
        {
            _validationResult = validationResult;
            _osoby = osoby ?? new List<Osoba>();
        }

        public string ValidateData(Osoba osoba)
        {
            return _validationResult;
        }

        public bool IsValidEmail(string email) => true;

        public bool IsValidPhoneNumber(string phoneNumber) => true;

        public IQueryable<Osoba> PobierzWszystkie()
        {
            return _osoby.AsQueryable();
        }

        public Osoba GetOsobaById(int id)
        {
            return _osoby.FirstOrDefault(o => o.Id == id);
        }

        public Osoba GetOsobaByLogin(string login)
        {
            return _osoby.FirstOrDefault(o => o.Login == login);
        }

        public Osoba GetOsobaByEmail(string email)
        {
            return _osoby.FirstOrDefault(o => o.Email == email);
        }

        public Osoba GetOsobaByPhoneNumber(string phoneNumber)
        {
            return _osoby.FirstOrDefault(o => o.Telefon == phoneNumber);
        }

        public void Dodaj(Osoba osoba)
        {
            _osoby.Add(osoba);
        }

        public void Delete(int id)
        {
            var osoba = GetOsobaById(id);
            if (osoba != null)
            {
                _osoby.Remove(osoba);
            }
        }

        public void Update(Osoba osoba)
        {
            var existingOsoba = GetOsobaById(osoba.Id);
            if (existingOsoba != null)
            {
                existingOsoba.Login = osoba.Login;
                existingOsoba.Email = osoba.Email;
                existingOsoba.Telefon = osoba.Telefon;
                existingOsoba.Haslo = osoba.Haslo;
            }
        }

        public void Save()
        {

        }
    }
}
