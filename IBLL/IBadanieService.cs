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
        IQueryable<WykonaneBadania> PobierzWszystkie();
        WykonaneBadania GetBadanieById(int id);
        void Dodaj(WykonaneBadania badanie);
        void Delete(int id);
        void Update(WykonaneBadania badanie);
        void save();
    }
}
