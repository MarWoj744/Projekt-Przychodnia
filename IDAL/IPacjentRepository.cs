using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_
{
    public interface IPacjentRepository
    {
        IQueryable<Pacjent> PobierzWszystkie();
        Pacjent GetPacjentById(int id);
        void Dodaj(Pacjent pacjent);
        void Delete(int id);
        void Update(Pacjent pacjent);
        void save();
    }
}
