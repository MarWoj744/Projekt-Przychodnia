using IBLL;
using IDAL_;
using Models;
using System;
using System.Linq;

namespace BLL
{
    public class HarmonogramService : IHarmonogramService
    {
        private readonly IHarmonogramRepository _repo;

        public HarmonogramService(IHarmonogramRepository repo)
        {
            _repo = repo;
        }

        public IQueryable<Harmonogram> PobierzWszystkie()
        {
            return _repo.GetAll();
        }

        public IQueryable<Harmonogram> PobierzPoLekarzId(int lekarzId)
        {
            return _repo.GetByLekarzId(lekarzId);
        }

        public Harmonogram GetHarmonogramById(int id)
        {
            return _repo.GetById(id);
        }

        public void Dodaj(Harmonogram harmonogram)
        {
            _repo.Dodaj(harmonogram);
        }

        public void Update(Harmonogram harmonogram)
        {
            _repo.Update(harmonogram);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        public void Save()
        {
            _repo.Save();
        }

        public bool CzyTerminDostepny(int lekarzId, DateTime dataOd, DateTime dataDo)
        {
            //sprawdzenie czy termin koliduje z istniejącymi harmonogramami
            return !_repo.GetByLekarzId(lekarzId)
                         .Any(h => (dataOd < h.DataDo) && (dataDo > h.DataOd));
        }
    }
}
