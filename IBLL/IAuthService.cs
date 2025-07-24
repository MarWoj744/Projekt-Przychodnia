using DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto);
        Task<AuthResponseDTO?> RegisterAsync(RegisterDTO registerDto);
        Task<AuthResponseDTO?> RefreshTokenAsync(string refreshToken);
        Task<bool> LogoutAsync(int userId);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto);
        Task<bool> IsUserActiveAsync(int userId);
        string GenerateJwtToken(Osoba user);
        string GenerateRefreshToken();
        Task<Osoba?> ValidateRefreshTokenAsync(string refreshToken);
    }
}
