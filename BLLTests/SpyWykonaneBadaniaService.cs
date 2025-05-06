using DTOs;
using IBLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTests
{
    class SpyWykonaneBadaniaService : IWykonaneBadanieService
    {
        public int LicznikWywolan { get; private set; } = 0;
        public WykonaneBadaniaDTO LastDto { get; private set; }

        public Task DodajWykonaneBadanieAsync(WykonaneBadaniaDTO dto)
        {
            LicznikWywolan++;
            LastDto = dto;
            return Task.CompletedTask;
        }
    }
}
