using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Index(nameof(PESEL), IsUnique = true)]
    public class Pacjent : Osoba
    {
        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "błędna długość numeru PESEL")]

        public string PESEL { get; set; }

        public ICollection<Wizyta> Wizyty { get; set; } = new List<Wizyta>();
    }
}
