using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using IBLL;
using Models;
using Przychodnia.Repositories;

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
            //walidacje
            if (dto.DataWizyty < DateTime.Now)
                throw new Exception("Podane błędną datę");

            //utworzenie wizyty
            var wizyta = new Wizyta
            {
                PacjentId = dto.PacjentId,
                LekarzId = dto.LekarzId,
                RecepcjonistkaId = dto.RecepcjonistkaId,
                Data = dto.DataWizyty,
                Opis = dto.Opis
            };

            _wizytaRepo.dodaj(wizyta);

            return true;
        }

        public IQueryable<Wizyta> GetAll()
        {
            return _wizytaRepo.PobierzWszystkie();
        }
    }
}
