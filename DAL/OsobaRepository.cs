using Microsoft.EntityFrameworkCore;
using Przychodnia.Models;

namespace Przychodnia.Repositories
{
    public class OsobaRepository:IOsobaRepository
    {
        private readonly DbPrzychodnia _context;
        public OsobaRepository(DbPrzychodnia context)
        {
            _context = context;
        }
        public Osoba GetOsobaByLogin(string login)
        {
            return _context.Osoby.FirstOrDefault(o => o.Login == login);
        }
        public Osoba GetOsobaByEmail(string email)
        {
            return _context.Osoby.FirstOrDefault(o => o.Email == email);
        }
        public Osoba GetOsobaByPhoneNumber(string phoneNumber)
        {
            return _context.Osoby.FirstOrDefault(o => o.Telefon == phoneNumber);
        }

        public IQueryable<Osoba> PobierzWszystkie()
        {
            throw new NotImplementedException();
        }

        public Osoba GetOsobaById(int id)
        {
            throw new NotImplementedException();
        }

        public void Dodaj(Osoba osoba)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Osoba osoba)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }
    }
}
