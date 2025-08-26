using DTOs;
using IDAL_;
using Models;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class HarmonogramService : IHarmonogramService
    {
        private readonly IHarmonogramRepository _repo;

        public HarmonogramService(IHarmonogramRepository repo)
        {
            _repo = repo;
        }



        public IEnumerable<HarmonogramDTO> PobierzWszystkie()
        {
            return _repo.GetAll().Select(h => new HarmonogramDTO
            {
                Id = h.Id,
                LekarzId = h.LekarzId,
                DataOd = h.DataOd,
                DataDo = h.DataDo,
                Opis = h.Opis
            });
        }

        public IEnumerable<HarmonogramDTO> PobierzPoLekarzId(int lekarzId)
        {
            return _repo.GetByLekarzId(lekarzId).Select(h => new HarmonogramDTO
            {
                Id = h.Id,
                LekarzId = h.LekarzId,
                DataOd = h.DataOd,
                DataDo = h.DataDo,
                Opis = h.Opis
            });
        }


        public HarmonogramDTO PobierzPoId(int id)
        {
            var h = _repo.GetById(id);
            if (h == null) return null;

            return new HarmonogramDTO
            {
                Id = h.Id,
                LekarzId = h.LekarzId,
                DataOd = h.DataOd,
                DataDo = h.DataDo,
                Opis = h.Opis
            };
        }


        public void Dodaj(HarmonogramDTO dto)
        {
            var h = new Harmonogram
            {
                LekarzId = dto.LekarzId,
                DataOd = dto.DataOd,
                DataDo = dto.DataDo,
                Opis = dto.Opis
            };

            _repo.Dodaj(h);
            _repo.Save();
        }


        public void Aktualizuj(HarmonogramDTO dto)
        {
            var h = _repo.GetById(dto.Id);
            if (h == null)
                throw new KeyNotFoundException("Harmonogram nie istnieje");

            h.LekarzId = dto.LekarzId;
            h.DataOd = dto.DataOd;
            h.DataDo = dto.DataDo;
            h.Opis = dto.Opis;

            _repo.Update(h);
            _repo.Save();
        }

        public void Usun(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }

    }

}











       

       

      
