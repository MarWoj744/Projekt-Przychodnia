using DTOs;
using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Mapper;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsobaController : ControllerBase
    {
        private readonly IOsobaService _osobaService;
        private readonly Mapper map;

        public OsobaController(IOsobaService osobaService)
        {
            _osobaService = osobaService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var osoby = _osobaService.PobierzWszystkie().ToList();
            return Ok(osoby);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var osoba = _osobaService.GetOsobaById(id);
            if (osoba == null)
                return NotFound();

            return Ok(osoba);
        }


        [HttpPost]
        public IActionResult Create([FromBody] OsobaDTO osoba)
        {

            Osoba os = map.osobaToEntity(osoba);
            if (os == null)
                return BadRequest("Obiekt Osoba jest pusty.");

            var validation = _osobaService.ValidateData(os);
            if (validation != "Walidacja zakończona sukcesem.")
                return BadRequest(validation);

            _osobaService.Dodaj(os);
            _osobaService.Save();
            return CreatedAtAction(nameof(GetById), new { id = os.Id }, os);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] OsobaDTO osoba)
        {
            Osoba os = map.osobaToEntity(osoba);
            if (os == null || os.Id != id)
                return BadRequest("Niepoprawne dane.");

            var existing = _osobaService.GetOsobaById(id);
            if (existing == null)
                return NotFound();

            _osobaService.Update(os);
            _osobaService.Save();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var osoba = _osobaService.GetOsobaById(id);
            if (osoba == null)
                return NotFound();

            _osobaService.Delete(id);
            _osobaService.Save();
            return NoContent();
        }


        [HttpGet("email/{email}")]
        public IActionResult GetByEmail(string email)
        {
            var osoba = _osobaService.GetOsobaByEmail(email);
            if (osoba == null)
                return NotFound();
            return Ok(osoba);
        }


        [HttpGet("login/{login}")]
        public IActionResult GetByLogin(string login)
        {
            var osoba = _osobaService.GetOsobaByLogin(login);
            if (osoba == null)
                return NotFound();
            return Ok(osoba);
        }


        [HttpGet("phone/{phoneNumber}")]
        public IActionResult GetByPhoneNumber(string phoneNumber)
        {
            var osoba = _osobaService.GetOsobaByPhoneNumber(phoneNumber);
            if (osoba == null)
                return NotFound();
            return Ok(osoba);
        }
    }
}
