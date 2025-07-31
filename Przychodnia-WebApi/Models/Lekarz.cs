using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
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
