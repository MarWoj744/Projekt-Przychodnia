using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class WykonaneBadaniaDTO
    {
        [Required]
        public int WizytaId { get; set; }

        [Required]
        public int BadanieId { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [StringLength(500)]
        public string Wyniki { get; set; }

        [AllowNull]
        public string Zalecenia { get; set; }
    }
}
