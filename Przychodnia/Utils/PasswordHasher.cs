using BCrypt.Net;

namespace Przychodnia.Utils
{
    public static class PasswordHasher
    {
        //TODO Install-Package BCrypt.Net-Next
        //metoda do hashowania has�a
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        //metoda do por�wnania has�a przy logowaniu z hashowanym has�em w bazie danych
        public static bool VerifyPassword(string password, string hashedPassword) 
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}