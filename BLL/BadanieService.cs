using IBLL;
using Models;
using System.Linq;
using IDAL_;

namespace BLL
{
    public class BadanieService : IBadanieService
    {
        private readonly IBadanieRepository _badanieRepo;

        public BadanieService(IBadanieRepository badanieRepo)
        {
            _badanieRepo = badanieRepo;
        }

        public IQueryable<Badanie> PobierzWszystkie()
        {
            return _badanieRepo.PobierzWszystkie();
        }

        public Badanie GetBadanieById(int id)
        {
            return _badanieRepo.GetBadanieById(id);
        }

        public void Dodaj(Badanie badanie)
        {
            _badanieRepo.Dodaj(badanie);
        }

        public void Delete(int id)
        {
            _badanieRepo.Delete(id);
        }

        public void Update(Badanie badanie)
        {
            _badanieRepo.Update(badanie);
        }

        public void save()
        {
            _badanieRepo.save();
        }
    }
}
