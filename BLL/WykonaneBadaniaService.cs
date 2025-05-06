using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using Przychodnia.Repositories;
using DTOs;
using Models;

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

        public async Task DodajWykonaneBadanieAsync(WykonaneBadaniaDTO dto)
        {
            //sprawdzanie czy wizyta oraz badanie istnieja
            var wizyta = _wiztaRepo.getWizytaById(dto.WizytaId);

            if (wizyta == null)
            {
                throw new Exception("Wizyta nie istnieje.");
            }

            var badanie = _badaniaRepo.GetWykonaneBadaniaById(dto.BadanieId);
            if (badanie == null)
            {
                throw new Exception("Badanie nie istnieje.");
            }

            //specjalizacja lekarza
            if (wizyta.Lekarz.Specjalizacja != badanie.Badanie.Nazwa)
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

            _badaniaRepo.dodaj(wykonane);
        }
    }
}
