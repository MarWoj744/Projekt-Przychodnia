using Microsoft.AspNetCore.Http.HttpResults;
using Przychodnia.Models;
using Przychodnia.Repositories;
using System.Text.RegularExpressions;

namespace Przychodnia.Services
{
    public class OsobaService : IOsobaService
    {
        //Doda� repozytorium
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

            if( string.IsNullOrEmpty(osoba.Login) || string.IsNullOrEmpty(osoba.Haslo))
            {
                return "Login lub has�o jest nullem.";
            }

            // Sprawdzenie, czy login jest ju� w bazie danych
            //Dodac metode do repozytorium GetOsobaByLogin
            if (_osobaRepository.GetOsobaByLogin(osoba.Login) != null)
            {
                return "Login zajety.";
            }

            // Sprawdzenie, czy email jest ju� w bazie danych
            //Dodac metode do repozytorium GetOsobaByEmail
            if (_osobaRepository.GetOsobaByEmail(osoba.Email) != null)
            {
                return "Email jest zajety.";
            }


            // Walidacja numeru telefonu
            if (!IsValidPhoneNumber(osoba.Telefon))
            {
                return "Numer telefonu nie prawid�owy.";
            }

            if (!IsValidEmail(osoba.Email))
            {
                return "Email jest nieprawid�owy";
            }
            // Sprawdzenie, czy numer telefonu jest ju� w bazie danych
            //Dodac metode do repozytorium GetOsobaByPhoneNumber
            if (_osobaRepository.GetOsobaByPhoneNumber(osoba.Telefon) != null)
            {
                return "Numer telefonu zaj�ty.";
            }

            return "Walidacja zako�czona sukcesem.";
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Numer telefonu powinien mie� 9 cyfr i nie mo�e zawiera� liter
            return Regex.IsMatch(phoneNumber, @"^\d{9}$");
        }

        private bool IsValidEmail(string email)
        {
            //validacja emaila czy ma strukture nazwa@email.pl
            if (string.IsNullOrWhiteSpace(email)) { return false; }
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
    }
}