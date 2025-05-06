using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace IBLL
{
    public interface IOsobaService
    {
        string ValidateData(Osoba osoba);
        bool IsValidEmail(string email);
        bool IsValidPhoneNumber(string phoneNumber);
    }
}
