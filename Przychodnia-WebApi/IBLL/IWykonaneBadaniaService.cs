using System.Collections.Generic;
using DTOs;
using Models;

namespace IBLL
{
    public interface IWykonaneBadanieService
    {
        IEnumerable<WykonaneBadania> GetAll();
        WykonaneBadania GetById(int id);
        IEnumerable<WykonaneBadania> GetByPacjentId(int id);
        void Dodaj(WykonaneBadania badanieDto);
        void Update(WykonaneBadania badanieDto);
        void Delete(int id);
        void Save();
    }
}