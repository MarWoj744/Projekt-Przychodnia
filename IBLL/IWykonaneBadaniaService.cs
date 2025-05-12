using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace IBLL
{
    public interface IWykonaneBadanieRepository
    {
        Task DodajWykonaneBadanieAsync(WykonaneBadaniaDTO dto);
    }
}
