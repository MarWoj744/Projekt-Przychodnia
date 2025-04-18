using BCrypt.Net;

namespace Przychodnia.Utils
{
    public static class PasswordHasher
    {
        //TODO Install-Package BCrypt.Net-Next
        //metoda do hashowania has³a
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        //metoda do porównania has³a przy logowaniu z hashowanym has³em w bazie danych
        public static bool VerifyPassword(string password, string hashedPassword) 
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}