using DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ILekarzService
    {
        LekarzDTO GetLekarzById(int id);
        IQueryable<LekarzDTO> PobierzWszystkie();
        void Dodaj(LekarzDTO lekarz);
        void Delete(int id);
        void Update(LekarzDTO lekarz);
        void save();
    }
}
