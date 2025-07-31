using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IRecepcjonistkaService
    {
        IQueryable<Recepcjonistka> PobierzWszystkie();
        Recepcjonistka GetRecepcjonistkaById(int id);
        void Dodaj(Recepcjonistka recepcjonistka);
        void Delete(int id);
        void Update(Recepcjonistka recepcjonistka);
        void save();
    }
}
