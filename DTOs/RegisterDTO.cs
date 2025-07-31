using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(50)]
        public string Imie { get; set; }

        [Required]
        [StringLength(100)]
        public string Nazwisko { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression(@"^\d{11}$")]
        public string PESEL { get; set; }

        [Required]
        [StringLength(200)]
        public string Adres { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        public string Telefon { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$")]
        public string Haslo { get; set; }

        [Required]
        [Compare("Haslo")]
        public string PotwierdzHaslo { get; set; }

        [Required]
        public Rola Rola { get; set; }
    }
}
