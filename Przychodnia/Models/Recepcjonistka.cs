using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Models
{
    public class Recepcjonistka : Osoba
    {
        [Required]
        [StringLength(30)]
        public string Login { get; set; }

        [Required]
        [StringLength(100)]
        public string Haslo { get; set; }

        public ICollection<Wizyta> WizytyZarejestrowane { get; set; }
    }
}