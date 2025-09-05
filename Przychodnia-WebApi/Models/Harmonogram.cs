using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Harmonogram
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("LekarzId")]
        public int LekarzId { get; set; }
        public Lekarz Lekarz { get; set; }
        [Required]
        public DateTime DataOd { get; set; }
        [Required]
        public DateTime DataDo { get; set; }

        public string? Opis { get; set; }
    }
}
