using IBLL;
using Models;
using System;
using System.Linq;
using IDAL_;

namespace BLL
{
    public class RecepcjonistkaService : IRecepcjonistkaService
    {
        private readonly IRecepcjonistkaRepository _recepcjonistkaRepo;

        public RecepcjonistkaService(IRecepcjonistkaRepository recepcjonistkaRepo)
        {
            _recepcjonistkaRepo = recepcjonistkaRepo;
        }

        public IQueryable<Recepcjonistka> PobierzWszystkie()
        {
            return _recepcjonistkaRepo.PobierzWszystkie();
        }

        public Recepcjonistka GetRecepcjonistkaById(int id)
        {
            return _recepcjonistkaRepo.GetRecepcjonistkaById(id);
        }

        public void Dodaj(Recepcjonistka recepcjonistka)
        {
            recepcjonistka.Haslo = PasswordHasher.HashPassword(recepcjonistka.Haslo);
            _recepcjonistkaRepo.Dodaj(recepcjonistka);
        }

        public void Delete(int id)
        {
            _recepcjonistkaRepo.Delete(id);
        }

        public void Update(Recepcjonistka recepcjonistka)
        {
            _recepcjonistkaRepo.Update(recepcjonistka);
        }

        public void save()
        {
            _recepcjonistkaRepo.save();
        }
    }
}

