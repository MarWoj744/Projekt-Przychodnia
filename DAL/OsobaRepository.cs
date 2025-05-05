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

        public IQueryable<Osoba> PobierzWszystkie()
        {
            return _context.Osoby;
        }

        public Osoba GetOsobaById(int id)
        {
            return _context.Osoby.FirstOrDefault(o => o.Id == id);
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
            return _context.Osoby.FirstOrDefault(o => o.PhoneNumber == phoneNumber);
        }

        public void Dodaj(Osoba osoba)
        {
            _context.Osoby.Add(osoba);
        }

        public void Delete(int id)
        {
            var osoba = GetOsobaById(id);
            if (osoba != null)
            {
                _context.Osoby.Remove(osoba);
            }
        }

        public void Update(Osoba osoba)
        {
            _context.Osoby.Update(osoba);
        }
       public void save()
        {
            _context.SaveChanges();
        }
    }
}



        