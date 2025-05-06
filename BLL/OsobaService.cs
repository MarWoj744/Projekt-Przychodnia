using IBLL;
using Models;
using Przychodnia.Repositories;
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
        //Dodać repozytorium
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

            // Sprawdzenie, czy login jest już w bazie danych
            //Dodac metode do repozytorium GetOsobaByLogin
            if (_osobaRepository.GetOsobaByLogin(osoba.Login) != null)
            {
                return "Login zajety.";
            }

            // Sprawdzenie, czy email jest już w bazie danych
            //Dodac metode do repozytorium GetOsobaByEmail
            if (_osobaRepository.GetOsobaByEmail(osoba.Email) != null)
            {
                return "Email jest zajety.";
            }


            // Walidacja numeru telefonu
            if (!IsValidPhoneNumber(osoba.Telefon))
            {
                return "Numer telefonu nie prawidłowy.";
            }

            if (!IsValidEmail(osoba.Email))
            {
                return "Email jest nieprawidłowy";
            }
            // Sprawdzenie, czy numer telefonu jest już w bazie danych
            //Dodac metode do repozytorium GetOsobaByPhoneNumber
            if (_osobaRepository.GetOsobaByPhoneNumber(osoba.Telefon) != null)
            {
                return "Numer telefonu zajęty.";
            }

            return "Walidacja zakończona sukcesem.";
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            // Numer telefonu powinien mieć 9 cyfr i nie może zawierać liter
            return Regex.IsMatch(phoneNumber, @"^\d{9}$");
        }

        public bool IsValidEmail(string email)
        {
            //validacja emaila czy ma strukture nazwa@email.pl
            if (string.IsNullOrWhiteSpace(email)) { return false; }
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
    }
}
