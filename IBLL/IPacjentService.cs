using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IPacjentService
    {
        string ValidatePesel(Pacjent pacjent);
        bool IsValidPesel(string pesel);
    }
}
