using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class OsobaDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Imie { get; set; }

        [Required]
        public string Nazwisko { get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        public string Email { get; set; }

        [Phone]
        public string Telefon { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Haslo { get; set; }

        [Required]
        public Rola Rola { get; set; }
    }
}
