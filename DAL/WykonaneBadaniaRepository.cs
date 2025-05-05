using Microsoft.EntityFrameworkCore;
using Przychodnia.Models;

namespace Przychodnia.Repositories
{
    public class WykonaneBadaniaRepository : IWykonaneBadaniaRepository
    {
        private readonly DbPrzychodnia _context;

        public WykonaneBadaniaRepository(DbPrzychodnia context)
        {
            _context = context;
        }

        public IQueryable<WykonaneBadania> GetAll()
        {
            return _context.WykonaneBadania;
        }

        public WykonaneBadania GetWykonaneBadaniaById(int id)
        {
            return _context.WykonaneBadania.FirstOrDefault(b => b.Id == id);
        }

        public void dodaj(WykonaneBadania badania)
        {
            _context.WykonaneBadania.Add(badania);
        }

        public void update(WykonaneBadania badania)
        {
            _context.WykonaneBadania.Update(badania);
        }

        public void delete(int id)
        {
            var badanie = GetWykonaneBadaniaById(id);
            if (badanie != null)
            {
                _context.WykonaneBadania.Remove(badanie);
            }
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
