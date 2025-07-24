using DTOs;
using IBLL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace BLL
{
    public class AuthService : IAuthService
    {
        private readonly IOsobaService _osobaService;
        private readonly IConfiguration _configuration;

        public AuthService(IOsobaService osobaService, IConfiguration configuration)
        {
            _osobaService = osobaService;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto)
        {
            var user = _osobaService.GetOsobaByLogin(loginDto.Login);
            if (user == null || !user.IsActive)
                return null;

            if (!PasswordHasher.VerifyPassword(loginDto.Haslo, user.Haslo))
                return null;

            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            _osobaService.Update(user);
            _osobaService.Save();

            return new AuthResponseDTO
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                UserId = user.Id,
                Login = user.Login,
                Imie = user.Imie,
                Nazwisko = user.Nazwisko,
                Email = user.Email,
                Rola = user.Rola,
                TokenExpiration = DateTime.UtcNow.AddMinutes(GetTokenExpirationMinutes()),
                IsActive = user.IsActive
            };
        }

        public async Task<AuthResponseDTO?> RegisterAsync(RegisterDTO registerDto)
        {
            if (_osobaService.GetOsobaByLogin(registerDto.Login) != null)
                return null;

            if (_osobaService.GetOsobaByEmail(registerDto.Email) != null)
                return null;

            Osoba newUser = registerDto.Rola switch
            {
                Rola.Pacjent => new Pacjent
                {
                    Imie = registerDto.Imie,
                    Nazwisko = registerDto.Nazwisko,
                    PESEL = registerDto.PESEL,
                    Adres = registerDto.Adres,
                    Telefon = registerDto.Telefon,
                    Email = registerDto.Email,
                    Login = registerDto.Login,
                    Haslo = PasswordHasher.HashPassword(registerDto.Haslo),
                    Rola = registerDto.Rola,
                    IsActive = true
                },
                Rola.Lekarz => new Lekarz
                {
                    Imie = registerDto.Imie,
                    Nazwisko = registerDto.Nazwisko,
                    Adres = registerDto.Adres,
                    Telefon = registerDto.Telefon,
                    Email = registerDto.Email,
                    Login = registerDto.Login,
                    Haslo = PasswordHasher.HashPassword(registerDto.Haslo),
                    Rola = registerDto.Rola,
                    IsActive = true,
                    Tytul = "dr", // domyślne
                    Specjalizacja = "Ogólna" // domyślne
                },
                Rola.Recepcjonistka => new Recepcjonistka
                {
                    Imie = registerDto.Imie,
                    Nazwisko = registerDto.Nazwisko,
                    Adres = registerDto.Adres,
                    Telefon = registerDto.Telefon,
                    Email = registerDto.Email,
                    Login = registerDto.Login,
                    Haslo = PasswordHasher.HashPassword(registerDto.Haslo),
                    Rola = registerDto.Rola,
                    IsActive = true
                },
                _ => throw new ArgumentException("Nieprawidłowa rola")
            };

            _osobaService.Dodaj(newUser);
            _osobaService.Save();

            var loginDto = new LoginDTO
            {
                Login = registerDto.Login,
                Haslo = registerDto.Haslo
            };

            return await LoginAsync(loginDto);
        }

        public async Task<AuthResponseDTO?> RefreshTokenAsync(string refreshToken)
        {
            var user = await ValidateRefreshTokenAsync(refreshToken);
            if (user == null || !user.IsActive)
                return null;

            var newAccessToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            _osobaService.Update(user);
            _osobaService.Save();

            return new AuthResponseDTO
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                UserId = user.Id,
                Login = user.Login,
                Imie = user.Imie,
                Nazwisko = user.Nazwisko,
                Email = user.Email,
                Rola = user.Rola,
                TokenExpiration = DateTime.UtcNow.AddMinutes(GetTokenExpirationMinutes()),
                IsActive = user.IsActive
            };
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            var user = _osobaService.GetOsobaById(userId);
            if (user == null)
                return false;

            user.RefreshToken = null;
            _osobaService.Update(user);
            _osobaService.Save();

            return true;
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto)
        {
            var user = _osobaService.GetOsobaById(userId);
            if (user == null || !user.IsActive)
                return false;

            if (!PasswordHasher.VerifyPassword(changePasswordDto.StareHaslo, user.Haslo))
                return false;

            user.Haslo = PasswordHasher.HashPassword(changePasswordDto.NoweHaslo);
            _osobaService.Update(user);
            _osobaService.Save();

            return true;
        }

        public async Task<bool> IsUserActiveAsync(int userId)
        {
            var user = _osobaService.GetOsobaById(userId);
            return user?.IsActive ?? false;
        }

        public string GenerateJwtToken(Osoba user)
        {
            var jwtSettings = _configuration.GetSection("JWT");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Rola.ToString()),
                new Claim("Imie", user.Imie),
                new Claim("Nazwisko", user.Nazwisko)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(GetTokenExpirationMinutes()),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public async Task<Osoba?> ValidateRefreshTokenAsync(string refreshToken)
        {
            var users = _osobaService.PobierzWszystkie();
            return users.FirstOrDefault(u => u.RefreshToken == refreshToken && u.IsActive);
        }

        private int GetTokenExpirationMinutes()
        {
            var jwtSettings = _configuration.GetSection("JWT");
            return int.Parse(jwtSettings["ExpiresInMinutes"] ?? "60");
        }
    }
}
