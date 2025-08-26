using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Models
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
        public int WizytaId { get; set; }
        [ForeignKey("WizytaId")]
        public Wizyta Wizyta { get; set; }

        [Required]
        public int BadanieId { get; set; }
        [ForeignKey("BadanieId")]
        public Badanie Badanie { get; set; }
        [AllowNull]
        public string Zalecenia {  get; set; }
    }
}
