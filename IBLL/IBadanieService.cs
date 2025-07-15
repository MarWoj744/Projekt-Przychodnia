using DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IBadanieService
    {
        IQueryable<WykonaneBadaniaDTO> PobierzWszystkie();
        WykonaneBadaniaDTO GetBadanieById(int id);
        void Dodaj(WykonaneBadaniaDTO badanie);
        void Delete(int id);
        void Update(WykonaneBadaniaDTO badanie);
        void save();
    }
}
