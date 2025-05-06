using DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IWizytaService
    {
        Task<bool> ZarejestrujWizyteAsync(RejestracjaWizytyDTO dto);
        IQueryable<Wizyta> GetAll();
    }
}
