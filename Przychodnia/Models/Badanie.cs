using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Models
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

        public ICollection<WykonaneBadania> Wykonane { get; set; }
    }
}