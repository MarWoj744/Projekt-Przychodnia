using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Models
{
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
        [RegularExpression(@"^\d{11}$", ErrorMessage = "błędna długość numeru PESEL")]
        public string PESEL { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Telefon { get; set; }
    }
}
