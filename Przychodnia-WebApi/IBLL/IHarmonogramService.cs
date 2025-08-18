using DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IHarmonogramService
    {
        IEnumerable<HarmonogramDTO> PobierzWszystkie();
        IEnumerable<HarmonogramDTO> PobierzPoLekarzId(int lekarzId);
        HarmonogramDTO PobierzPoId(int id);
        void Dodaj(HarmonogramDTO dto);
        void Aktualizuj(HarmonogramDTO dto);
        void Usun(int id);


     
    }
}


