using System;
using System.Linq;
using System.Threading.Tasks;
using DTOs;
using IBLL;
using IDAL_;
using Models;

namespace BLL
{
    public class WizytaService : IWizytaService
    {
        private readonly IWizytaRepository _wizytaRepo;

        public WizytaService(IWizytaRepository wizytaRepo)
        {
            _wizytaRepo = wizytaRepo;
        }

        public async Task<bool> ZarejestrujWizyteAsync(RejestracjaWizytyDTO dto)
        {
            // Walidacje
            if (dto.DataWizyty < DateTime.Now)
                throw new Exception("Podano błędną datę");

            // Utworzenie wizyty
            var wizyta = new Wizyta
            {
                PacjentId = dto.PacjentId,
                LekarzId = dto.LekarzId,
                RecepcjonistkaId = dto.RecepcjonistkaId,
                Data = dto.DataWizyty,
                Opis = dto.Opis
            };

            // Dodanie wizyty do repozytorium
            _wizytaRepo.dodaj(wizyta);
            _wizytaRepo.save(); // Zapisanie zmian

            return true;
        }

        public IQueryable<Wizyta> GetAll()
        {
            return _wizytaRepo.PobierzWszystkie();
        }

        public Wizyta GetWizytaById(int id)
        {
            return _wizytaRepo.getWizytaById(id);
        }

        public async Task<bool> UpdateWizytaAsync(Wizyta wizyta)
        {
            _wizytaRepo.update(wizyta);
            _wizytaRepo.save(); // Zapisanie zmian
            return true;
        }

        public async Task<bool> DeleteWizytaAsync(int id)
        {
            _wizytaRepo.delete(id);
            _wizytaRepo.save(); // Zapisanie zmian
            return true;
        }
    }
}
