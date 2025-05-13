using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IPacjentService
    {
        string ValidatePesel(Pacjent pacjent);
        bool IsValidPesel(string pesel);
        IQueryable<Pacjent> PobierzWszystkie();
        Pacjent GetPacjentById(int id);
        void Dodaj(Pacjent pacjent);
        void Delete(int id);
        void Update(Pacjent pacjent);
        void save();
    }
}
