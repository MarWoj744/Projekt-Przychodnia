using System;
using Microsoft.EntityFrameworkCore;
using Przychodnia.DTOs;
using Przychodnia.Models;
using Przychodnia.Repositories;

namespace Przychodnia.Services
{
    public class WykonaneBadaniaService : IWykonaneBadanieService
    {
        private readonly IWykonaneBadaniaRepository _badaniaRepo;

        public WykonaneBadaniaService(IWykonaneBadaniaRepository badaniaRepo)
        {
            _badaniaRepo = badaniaRepo;
        }

        public async Task DodajWykonaneBadanieAsync(WykonaneBadaniaDTO dto)
        {
            //sprawdzanie czy wizyta oraz badanie istnieja
            var wizyta = await _badaniaRepo.GetWizytaByIdAsync(dto.WizytaId);

            if (wizyta == null)
            {
                throw new Exception("Wizyta nie istnieje.");
            }

            var badanie = await _badaniaRepo.GetBadanieByIdAsync(dto.BadanieId);
            if (badanie == null)
            {
                throw new Exception("Badanie nie istnieje.");
            }

            //specjalizacja lekarza
            if (wizyta.Lekarz.Specjalizacja != badanie.Specjalizacja)
            {
                throw new Exception("Specjalizacja lekarza nie odpowiada badaniu.");
            }

            var wykonane = new WykonaneBadania
            {
                WizytaId = dto.WizytaId,
                BadanieId = dto.BadanieId,
                Data = dto.Data,
                Wyniki = dto.Wyniki
            };

            await _badaniaRepo.DodajAsync(wykonane);
        }
    }
}