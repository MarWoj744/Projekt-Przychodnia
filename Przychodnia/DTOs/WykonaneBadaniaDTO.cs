using System;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.DTOs
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
    }
}
