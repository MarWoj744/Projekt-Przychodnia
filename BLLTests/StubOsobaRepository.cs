using IBLL;
using IDAL_;
using Models;
<<<<<<< Updated upstream
<<<<<<< Updated upstream:BLLTests/StubOsobaService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> Stashed changes:BLLTests/StubOsobaRepository.cs
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> Stashed changes

namespace BLLTests
{
    class StubOsobaRepository : IOsobaRepository
    {
<<<<<<< Updated upstream
<<<<<<< Updated upstream:BLLTests/StubOsobaService.cs
        private readonly string _validationResult;

        public StubOsobaService(string validationResult)
        {
            _validationResult = validationResult;
        }

        public string ValidateData(Osoba osoba)
        {
            return _validationResult;
        }

        public bool IsValidEmail(string email) => true;

        public bool IsValidPhoneNumber(string phoneNumber) => true;
=======
        private readonly List<Osoba> _osoby;
        public StubOsobaRepository(List<Osoba> osoby = null)
        {
            _osoby = osoby ?? new List<Osoba>();
        }
=======
        private readonly List<Osoba> _osoby;
        public StubOsobaRepository(List<Osoba> osoby = null)
        {
            _osoby = osoby ?? new List<Osoba>();
        }

        public void save() { }
>>>>>>> Stashed changes

        public void save() { }

        public Osoba GetOsobaById(int id)
        {
            return _osoby.FirstOrDefault(o => o.Id == id);
        }

        public Osoba GetOsobaByLogin(string login)
        {
            return _osoby.FirstOrDefault(o => o.Login == login);
        }

        public Osoba GetOsobaByEmail(string email)
        {
            return _osoby.FirstOrDefault(o => o.Email == email);
        }

        public Osoba GetOsobaByPhoneNumber(string phoneNumber)
        {
            return _osoby.FirstOrDefault(o => o.Telefon == phoneNumber);
        }

        public IQueryable<Osoba> PobierzWszystkie()
        {
            return _osoby.AsQueryable();
        }

        public void Dodaj(Osoba osoba)
        {
            
        }

        public void Delete(int id)
        {
            
        }

        public void Update(Osoba osoba)
        {
            
        }
>>>>>>> Stashed changes:BLLTests/StubOsobaRepository.cs
    }
}
