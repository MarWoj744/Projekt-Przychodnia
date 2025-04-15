using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Models
{
    public class Recepcjonistka : Osoba
    {
        public ICollection<Wizyta> WizytyZarejestrowane { get; set; }
    }
}