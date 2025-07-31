using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class RecepcjonistkaDTO : OsobaDTO
    {
        public ICollection<RejestracjaWizytyDTO> WizytyZarejestrowane { get; set; }
    }
}
