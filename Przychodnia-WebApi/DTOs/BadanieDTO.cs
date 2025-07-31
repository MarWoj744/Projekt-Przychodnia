using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class BadanieDTO
    {
        [Required]
        public string Nazwa { get; set; }

        [Required]
        public decimal Cennik { get; set; }

        [Required]
        public string Specjalizacja { get; set; }

        public ICollection<WykonaneBadaniaDTO> Wykonane { get; set; }
    }
}
