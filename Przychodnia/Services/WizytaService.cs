using Przychodnia.DTOs;
using Przychodnia.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Repositories;

namespace Przychodnia.Services
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
                throw new Exception("Podane b³êdn¹ datê");

            var lekarzZajety = await _wizytaRepo.CzyLekarzMaZajetyTerminAsync(
                dto.LekarzId, 
                dto.DataWizyty
            );

            if (lekarzZajety)
                throw new Exception("Termin zajêty.");

            //utworzenie wizyty
            var wizyta = new Wizyta
            {
                PacjentId = dto.PacjentId,
                LekarzId = dto.LekarzId,
                RecepcjonistkaId = dto.RecepcjonistkaId,
                Data = dto.DataWizyty,
                Opis = dto.Opis
            };

            await _wizytaRepo.DodajWizyteAsync(wizyta);

            return true;
        }

        public async Task<List<Wizyta>> GetAllAsync()
        {
            return await _wizytaRepo.PobierzWszystkieAsync();
        }
    }
}