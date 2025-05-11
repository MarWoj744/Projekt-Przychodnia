using DTOs;
using IBLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTests
{
    class MockWizytaService : IWizytaService
    {
        private int _planowaneWywolania = 0;
        private int _licznikWywolan = 0;
        private List<Wizyta> _wizyty = new List<Wizyta>();

        public void UstawLiczbePlanowanychWywolan(int count)
        {
            _planowaneWywolania = count;
            _licznikWywolan = 0;
        }

        public Task<bool> ZarejestrujWizyteAsync(RejestracjaWizytyDTO dto)
        {
            _licznikWywolan++;
            var wizyta = new Wizyta
            {
                PacjentId = dto.PacjentId,
                LekarzId = dto.LekarzId,
                RecepcjonistkaId = dto.RecepcjonistkaId,
                Data = dto.DataWizyty,
                Opis = dto.Opis
            };
            _wizyty.Add(wizyta);
            return Task.FromResult(true);
        }

        public IQueryable<Wizyta> GetAll()
        {
            return _wizyty.AsQueryable();
        }

        public Wizyta GetWizytaById(int id)
        {
            return _wizyty.FirstOrDefault(w => w.Id == id); // Zakładając, że Wizyta ma właściwość Id
        }

        public Task<bool> UpdateWizytaAsync(Wizyta wizyta)
        {
            _licznikWywolan++;
            var existingWizyta = _wizyty.FirstOrDefault(w => w.Id == wizyta.Id);
            if (existingWizyta != null)
            {
                existingWizyta.PacjentId = wizyta.PacjentId;
                existingWizyta.LekarzId = wizyta.LekarzId;
                existingWizyta.RecepcjonistkaId = wizyta.RecepcjonistkaId;
                existingWizyta.Data = wizyta.Data;
                existingWizyta.Opis = wizyta.Opis;
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteWizytaAsync(int id)
        {
            _licznikWywolan++;
            var wizyta = _wizyty.FirstOrDefault(w => w.Id == id);
            if (wizyta != null)
            {
                _wizyty.Remove(wizyta);
            }
            return Task.FromResult(true);
        }

        public bool Weryfikacja()
        {
            return _planowaneWywolania == _licznikWywolan;
        }
    }
}
