using System.Collections.Generic;
using System.Linq;
using IBLL;
using DTOs;
using Models;
using IDAL_;

namespace BLL
{
    public class WykonaneBadaniaService : IWykonaneBadanieService
    {
        private readonly IWykonaneBadaniaRepository _badaniaRepo;
        private readonly IWizytaRepository _wiztaRepo;

        public WykonaneBadaniaService(IWykonaneBadaniaRepository badaniaRepo, IWizytaRepository wiztaRepo)
        {
            _badaniaRepo = badaniaRepo;
            _wiztaRepo = wiztaRepo;
        }

        public IEnumerable<WykonaneBadania> GetAll()
        {
            return _badaniaRepo.GetAll()
                .Select(b => new WykonaneBadania
                {
                    WizytaId = b.WizytaId,
                    BadanieId = b.BadanieId,
                    Data = b.Data,
                    Wyniki = b.Wyniki,
                    Zalecenia = b.Zalecenia,
                    PacjentId=b.PacjentId
                });
        }

        public WykonaneBadania GetById(int id)
        {
            var badanie = _badaniaRepo.GetWykonaneBadaniaById(id);
            if (badanie == null) return null;

            return new WykonaneBadania
            {
                WizytaId = badanie.WizytaId,
                BadanieId = badanie.BadanieId,
                Data = badanie.Data,
                Wyniki = badanie.Wyniki,
                Zalecenia = badanie.Zalecenia,
                PacjentId = badanie.PacjentId
            };
        }
        public IEnumerable<WykonaneBadania> GetByPacjentId(int id)
        {
            return _badaniaRepo.getByPacjentId(id);
        }
        public void Dodaj(WykonaneBadania dto)
        {
            var badanie = new WykonaneBadania
            {
                WizytaId = dto.WizytaId,
                BadanieId = dto.BadanieId,
                Data = dto.Data,
                Wyniki = dto.Wyniki,
                Zalecenia = dto.Zalecenia,
                PacjentId = dto.PacjentId

            };
            _badaniaRepo.dodaj(badanie);
        }

        public void Update(WykonaneBadania dto)
        {
            var badanie = _badaniaRepo.GetAll()
                .FirstOrDefault(b => b.WizytaId == dto.WizytaId && b.BadanieId == dto.BadanieId);

            if (badanie != null)
            {
                badanie.Data = dto.Data;
                badanie.Wyniki = dto.Wyniki;
                badanie.Zalecenia= dto.Zalecenia;
                _badaniaRepo.update(badanie);
            }
        }

        public void Delete(int id)
        {
            _badaniaRepo.delete(id);
        }

        public void Save()
        {
            _badaniaRepo.save();
        }
    }
}
