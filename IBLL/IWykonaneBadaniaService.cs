using System.Collections.Generic;
using DTOs;

namespace IBLL
{
    public interface IWykonaneBadanieRepository
    {
        IEnumerable<WykonaneBadaniaDTO> GetAll();
        WykonaneBadaniaDTO GetById(int id);
        void Dodaj(WykonaneBadaniaDTO badanieDto);
        void Update(WykonaneBadaniaDTO badanieDto);
        void Delete(int id);
        void Save();
    }
}