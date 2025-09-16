using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class WizytaWidokDTO
    {
        public int Id { get; set; }
        public string Pacjent { get; set; }  
        public string Lekarz { get; set; }   
        public string Badanie { get; set; }  
        public DateTime Data { get; set; }
        public string Godzina => Data.ToString("HH:mm");
        public string Status { get; set; }

    }
}
