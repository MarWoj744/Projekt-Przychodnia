using IBLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLTests
{
    class StubOsobaService : IOsobaService
    {
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
    }
}
