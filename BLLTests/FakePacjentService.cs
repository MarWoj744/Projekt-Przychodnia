using IBLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLLTests
{
    class FakePacjentService : IPacjentService
    {
        public string ValidatePesel(Pacjent pacjent)
        {
            if (pacjent == null || string.IsNullOrWhiteSpace(pacjent.PESEL))
                return "PESEL nie może być pusty.";

            return IsValidPesel(pacjent.PESEL) ? "PESEL jest poprawny." : "Nieprawidłowy numer PESEL.";
        }

        public bool IsValidPesel(string pesel)
        {
            if (!Regex.IsMatch(pesel, @"^\d{11}$")) { return false; }


            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int sum = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * (pesel[i] - '0');
            }

            int checksum = (10 - (sum % 10)) % 10;
            int lastDigit = pesel[10] - '0';

            return checksum == lastDigit;
        }
    }
}
