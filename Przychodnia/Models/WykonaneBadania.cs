using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Przychodnia.Models
{
    public class WykonaneBadania
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [StringLength(500)]
        public string Wyniki { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Cena { get; set; }

        [Required]
        public int WizytaId { get; set; }
        [ForeignKey("WizytaId")]
        public Wizyta Wizyta { get; set; }

        [Required]
        public int BadanieId { get; set; }
        [ForeignKey("BadanieId")]
        public Badanie Badanie { get; set; }
    }
}