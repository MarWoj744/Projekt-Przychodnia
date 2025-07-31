using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_
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
        Osoba GetOsobaByRefreshToken(string refreshToken);
        void UpdateRefreshToken(int userId, string refreshToken);
        void ClearRefreshToken(int userId);
    }
}
