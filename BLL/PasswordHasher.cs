using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class PasswordHasher
    {
        //TODO Install-Package BCrypt.Net-Next
        //metoda do hashowania hasła
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        //metoda do porównania hasła przy logowaniu z hashowanym hasłem w bazie danych
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
