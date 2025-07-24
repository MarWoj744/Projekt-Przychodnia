using IBLL;
using IDAL_;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class OsobaService : IOsobaService
    {
        private readonly IOsobaRepository _osobaRepository;

        public OsobaService(IOsobaRepository osobaRepository)
        {
            _osobaRepository = osobaRepository;
        }

        public string ValidateData(Osoba osoba)
        {
            if (osoba == null)
            {
                return "Osoba jest nullem.";
            }

            if (string.IsNullOrEmpty(osoba.Login) || string.IsNullOrEmpty(osoba.Haslo))
            {
                return "Login lub hasło jest nullem.";
            }

            if (_osobaRepository.GetOsobaByLogin(osoba.Login) != null)
            {
                return "Login zajęty.";
            }

            if (_osobaRepository.GetOsobaByEmail(osoba.Email) != null)
            {
                return "Email jest zajęty.";
            }

            if (!IsValidPhoneNumber(osoba.Telefon))
            {
                return "Numer telefonu nieprawidłowy.";
            }

            if (!IsValidEmail(osoba.Email))
            {
                return "Email jest nieprawidłowy.";
            }

            if (_osobaRepository.GetOsobaByPhoneNumber(osoba.Telefon) != null)
            {
                return "Numer telefonu zajęty.";
            }

            return "Walidacja zakończona sukcesem.";
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d{9}$");
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) { return false; }
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }

        // Implementacja nowych metod
        public IQueryable<Osoba> PobierzWszystkie()
        {
            return _osobaRepository.PobierzWszystkie();
        }

        public Osoba GetOsobaById(int id)
        {
            return _osobaRepository.GetOsobaById(id);
        }

        public Osoba GetOsobaByLogin(string login)
        {
            return _osobaRepository.GetOsobaByLogin(login);
        }

        public Osoba GetOsobaByEmail(string email)
        {
            return _osobaRepository.GetOsobaByEmail(email);
        }

        public Osoba GetOsobaByPhoneNumber(string phoneNumber)
        {
            return _osobaRepository.GetOsobaByPhoneNumber(phoneNumber);
        }

        public void Dodaj(Osoba osoba)
        {
            _osobaRepository.Dodaj(osoba);
        }

        public void Delete(int id)
        {
            _osobaRepository.Delete(id);
        }

        public void Update(Osoba osoba)
        {
            _osobaRepository.Update(osoba);
        }

        public void Save()
        {
            _osobaRepository.save();
        }
        public Osoba GetOsobaByRefreshToken(string refreshToken)
        {
            return _osobaRepository.GetOsobaByRefreshToken(refreshToken);
        }

        public void UpdateRefreshToken(int userId, string refreshToken)
        {
            _osobaRepository.UpdateRefreshToken(userId, refreshToken);
        }

        public void ClearRefreshToken(int userId)
        {
            _osobaRepository.ClearRefreshToken(userId);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return PasswordHasher.VerifyPassword(password, hashedPassword);
        }
    }
}
