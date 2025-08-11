using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Harmonogram
    {
        public int Id { get; set; }
        public int LekarzId { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public string Opis { get; set; }
    }
}
