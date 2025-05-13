using IBLL;
using Models;
using System.Linq;
using IDAL_;

namespace BLL
{
    public class LekarzService : ILekarzService
    {
        private readonly ILekarzRepository _lekarzRepo;

        public LekarzService(ILekarzRepository lekarzRepo)
        {
            _lekarzRepo = lekarzRepo;
        }

        public Lekarz GetLekarzById(int id)
        {
            return _lekarzRepo.GetLekarzById(id);
        }

        public IQueryable<Lekarz> PobierzWszystkie()
        {
            return _lekarzRepo.PobierzWszystkie();
        }

        public void Dodaj(Lekarz lekarz)
        {
            _lekarzRepo.Dodaj(lekarz);
        }

        public void Delete(int id)
        {
            _lekarzRepo.Delete(id);
        }

        public void Update(Lekarz lekarz)
        {
            _lekarzRepo.Update(lekarz);
        }

        public void save()
        {
            _lekarzRepo.save();
        }
    }
}
