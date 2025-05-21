using IDAL_;
using Models;
//using Przychodnia.Repositories;

namespace BLLTests
{
    class StubOsobaRepository:IOsobaRepository
    {
        private readonly List<Osoba> _osoby;
        public StubOsobaRepository(List<Osoba> osoby = null)
        {
            _osoby = osoby ?? new List<Osoba>();
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

        public IQueryable<Osoba> PobierzWszystkie()
        {
            return _osoby.AsQueryable();
        }

        public void Dodaj(Osoba osoba)
        {
            
        }

        public void Delete(int id)
        {
            
        }

        public void Update(Osoba osoba)
        {
            
        }
        public void save() { }
    }
}
