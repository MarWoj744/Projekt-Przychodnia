using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Przychodnia.Models
{
    public class Wizyta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [StringLength(500)]
        public string Opis { get; set; }

        [Required]
        public int PacjentId { get; set; }
        [ForeignKey("PacjentId")]
        public Pacjent Pacjent { get; set; }

        [Required]
        public int LekarzId { get; set; }
        [ForeignKey("LekarzId")]
        public Lekarz Lekarz { get; set; }

        [Required]
        public int RecepcjonistkaId { get; set; }
        [ForeignKey("RecepcjonistkaId")]
        public Recepcjonistka Recepcjonistka { get; set; }

        public ICollection<WykonaneBadania> Badania { get; set; }
    }
}