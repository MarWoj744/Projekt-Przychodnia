using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Models
{
    public class Lekarz : Osoba
    {
        [Required]
        [StringLength(10)]
        public string Tytul { get; set; }

        [Required]
        [StringLength(50)]
        public string Specjalizacja { get; set; }

        public ICollection<Wizyta> Wizyty { get; set; }
    }
}