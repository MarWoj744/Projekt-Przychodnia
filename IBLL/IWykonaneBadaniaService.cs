using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace IBLL
{
    public interface IWykonaneBadanieService
    {
        Task DodajWykonaneBadanieAsync(WykonaneBadaniaDTO dto);
    }
}
