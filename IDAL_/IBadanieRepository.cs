using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_
{
    public interface IBadanieRepository
    {
        IQueryable<Badanie> PobierzWszystkie();
        Badanie GetBadanieById(int id);
        void Dodaj(Badanie badanie);
        void Delete(int id);
        void Update(Badanie badanie);
        void save();
    }
}
