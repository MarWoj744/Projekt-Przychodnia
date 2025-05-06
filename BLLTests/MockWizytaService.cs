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

        public void UstawLiczbePlanowanychWywolan(int count)
        {
            _planowaneWywolania = count;
            _licznikWywolan = 0;
        }

        public Task<bool> ZarejestrujWizyteAsync(RejestracjaWizytyDTO dto)
        {
            _licznikWywolan++;
            return Task.FromResult(true);
        }

        public IQueryable<Wizyta> GetAll()
        {
            return new List<Wizyta>().AsQueryable();
        }

        public bool Weryfikacja()
        {
            return _planowaneWywolania == _licznikWywolan;
        }
    }
}
