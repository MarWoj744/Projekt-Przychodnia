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

        public async Task<bool> ZarejestrujWizyteAsync(Wizyta dto)
        {
            if (dto.Data < DateTime.Now)
                throw new Exception("Podano błędną datę");

            var wizyta = new Wizyta
            {
                PacjentId = dto.PacjentId,
                LekarzId = dto.LekarzId,
                RecepcjonistkaId = dto.RecepcjonistkaId,
                Data = dto.Data,
                Opis = dto.Opis
            };

            _wizytaRepo.dodaj(wizyta);
            _wizytaRepo.save();

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
            _wizytaRepo.save();
            return true;
        }

        public async Task<bool> DeleteWizytaAsync(int id)
        {
            _wizytaRepo.delete(id);
            _wizytaRepo.save();
            return true;
        }
        public IQueryable<Wizyta> GetWizytyPacjenta(int pacjentId)
        {
            return _wizytaRepo.GetWizytyPacjenta(pacjentId);
        }
        public IQueryable<Wizyta> GetWizytyLekarza(int lekarzId, DateTime start, DateTime end)
        {
            return _wizytaRepo.PobierzWizytyLekarza(lekarzId, start, end);
        }
        public IQueryable<Wizyta> GetWizytyAnulowane()
        {
            return _wizytaRepo.PobierzWizytyAnulowane();
        }

        public IQueryable<Wizyta> GetWizytyAnulowaneLekarza(int lekarzId)
        {
            return _wizytaRepo.PobierzWizytyAnulowaneLekarza(lekarzId);
        }

        public async Task<bool> AnulujWizyteAsync(int id)
        {
            var wizyta = _wizytaRepo.getWizytaById(id);
            if (wizyta == null) return false;

            wizyta.Status = StatusWizyty.Anulowana;
            _wizytaRepo.update(wizyta);
            _wizytaRepo.save();
            return true;
        }

    }
}
