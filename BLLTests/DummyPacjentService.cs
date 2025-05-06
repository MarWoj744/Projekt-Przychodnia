using IBLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTests
{
    class DummyPacjentService : IPacjentService
    {
        public string ValidatePesel(Pacjent pacjent)
        {
            throw new InvalidOperationException("Ta implementacja sewisu nie powinna być użyta");
        }

        public bool IsValidPesel(string pesel)
        {
            throw new InvalidOperationException("Ta implementacja sewisu nie powinna być użyta");
        }
    }
}
