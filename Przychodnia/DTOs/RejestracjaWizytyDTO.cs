using System;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.DTOs
{
    public class RejestracjaWizytyDTO
    {
        [Required]
        public int PacjentId { get; set; }

        [Required]
        public int LekarzId { get; set; }

        [Required]
        public int RecepcjonistkaId { get; set; }

        [Required]
        public DateTime DataWizyty { get; set; }

        [StringLength(500)]
        public string Opis { get; set; }
    }
}
