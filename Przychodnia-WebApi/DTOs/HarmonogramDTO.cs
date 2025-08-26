using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class HarmonogramDTO
    {
        public int Id { get; set; }
        public int LekarzId { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public string Opis { get; set; }
    }
}
