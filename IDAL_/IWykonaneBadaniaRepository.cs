using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_
{
    public interface IWykonaneBadaniaRepository
    {
        //Task<Wizyta?> GetWizytaByIdAsync(int id);
        //Task<Badanie?> GetBadanieByIdAsync(int id);
        //Task DodajAsync(WykonaneBadania wykonaneBadania);
        IQueryable<WykonaneBadania> GetAll();
        WykonaneBadania GetWykonaneBadaniaById(int id);
        void dodaj(WykonaneBadania badania);
        void update(WykonaneBadania badania);
        void delete(int id);
        void save();
    }
}
