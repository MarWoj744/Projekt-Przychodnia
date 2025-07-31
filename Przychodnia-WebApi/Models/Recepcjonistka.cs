using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Recepcjonistka : Osoba
    {
        public ICollection<Wizyta> WizytyZarejestrowane { get; set; }
    }
}
