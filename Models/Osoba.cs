using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum Rola
    {
        Pacjent,
        Recepcjonistka,
        Lekarz,
        Administrator
    }

    public abstract class Osoba
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Imie { get; set; }

        [Required]
        [StringLength(50)]
        public string Nazwisko { get; set; }

        [Required]
        [StringLength(100)]
        public string Adres { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Telefon { get; set; }

        [Required]
        public string Login { get; set; }

        //Haslo bedzie hashowane algorytmem bcrypt
        [Required]
        public string Haslo { get; set; }

        [Required]
        public Rola Rola { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [StringLength(500)]
        public string? RefreshToken { get; set; }

    }
}
