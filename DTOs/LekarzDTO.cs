using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class LekarzDTO : OsobaDTO
    {
        [Required]
        public string Tytul { get; set; }

        [Required]
        public string Specjalizacja { get; set; }

        public ICollection<RejestracjaWizytyDTO> Wizyty { get; set; }
    }
}
