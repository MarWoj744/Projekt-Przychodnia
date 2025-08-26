using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Badanie
    {
       [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nazwa { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Cennik { get; set; }

        [Required]
        [StringLength(50)]
        public string Specjalizacja { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<WykonaneBadania> Wykonane { get; set; }
    }
}
