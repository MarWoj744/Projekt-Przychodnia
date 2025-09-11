using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Models
{
    public enum StatusWizyty
    {
        Zaplanowana,
        Zrealizowana,
        Anulowana
    }

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

        public int RecepcjonistkaId { get; set; }
        [ForeignKey("RecepcjonistkaId")]
        public Recepcjonistka Recepcjonistka { get; set; }

        [Required]
        public StatusWizyty Status { get; set; } = StatusWizyty.Zaplanowana;

        public ICollection<WykonaneBadania> Badania { get; set; }
    }
}
