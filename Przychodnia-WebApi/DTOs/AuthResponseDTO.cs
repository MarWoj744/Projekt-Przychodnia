using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public int UserId { get; set; }

        public string Login { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public string Email { get; set; }

        public Rola Rola { get; set; }

        public DateTime TokenExpiration { get; set; }

        public bool IsActive { get; set; }
    }
}
