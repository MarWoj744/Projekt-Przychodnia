using Microsoft.AspNetCore.Mvc;
using Przychodnia.Models;
using Przychodnia.Services;
using Przychodnia.Utils;

namespace Przychodnia.Controllers
{
    [ApiController]
    [Route("api/osoba")]
    public class OsobaController : Controller
    {
        //Dodaæ repozytorium które bêdzie obs³ugiwaæ zapis i odczyt u¿ytkownika do bazy danych
        // private readonly OsobaRepository _osobaRepository;
        private IOsobaService _osobaService;


        public OsobaController(/*OsobaRepository osobaRepository*/IOsobaService osobaService)
        {
            _osobaService = osobaService;
            // _osobaRepository = osobaRepositor;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Osoba osoba)
        {

            _osobaService.ValidateData(osoba);
            // Hashowanie has³a
            osoba.Haslo = PasswordHasher.HashPassword(osoba.Haslo);

            // Zapis do bazy danych
            // _osobaRepository.AddOsoba(osoba);

            return Ok("Osoba zarejestrowana.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Osoba zalogowanaOsoba)
        {
            if (zalogowanaOsoba == null || string.IsNullOrEmpty(zalogowanaOsoba.Login) || string.IsNullOrEmpty(zalogowanaOsoba.Haslo))
            {
                return BadRequest("Brak danych logowania.");
            }

            //dodaæ metode wyszukania u¿ytkownika za pomoc¹ loginu
            // var osoba = _osobaRepository.GetOsobaByLogin(zalogowanaOsoba.Login);
            if (zalogowanaOsoba == null || !PasswordHasher.VerifyPassword(zalogowanaOsoba.Haslo, zalogowanaOsoba.Haslo))
            {
                return Unauthorized("Z³y Login lub Has³o.");
            }

            return Ok("Logowanie udane.");
        }

    }
}