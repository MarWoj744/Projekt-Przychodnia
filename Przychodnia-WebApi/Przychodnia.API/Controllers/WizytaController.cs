using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models;
using DTOs;
using System;
using System.Threading.Tasks;
using System.Linq;
using Models.Mapper;

namespace Przychodnia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WizytaController : ControllerBase
    {
        private readonly IWizytaService _service;
        private readonly Mapper map;

        public WizytaController(IWizytaService service)
        {
            _service = service;
            map = new Mapper();
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var wizyty = _service.GetAll().ToList();
            var result = wizyty.Select(w => map.WizytaToWidokDTO(w)).ToList();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var wizyta = _service.GetWizytaById(id);
            if (wizyta == null)
                return NotFound();

            return Ok(wizyta);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RejestracjaWizytyDTO dto)
        {
            Wizyta wiz = map.WizytaToEntity(dto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.ZarejestrujWizyteAsync(wiz);
            if (!result)
                return BadRequest("Nie udało się zarejestrować wizyty.");

            return Ok("Wizyta zarejestrowana.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RejestracjaWizytyDTO dto)
        {
            Wizyta wiz = map.WizytaToEntity(dto);
            if (id != wiz.Id)
                return BadRequest();

            var result = await _service.UpdateWizytaAsync(wiz);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteWizytaAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("harmonogram/{lekarzId}")]
        public IActionResult GetHarmonogramLekarza(int lekarzId, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var wizyty = _service.GetWizytyLekarza(lekarzId, start, end);
            return Ok(wizyty);
        }
        [HttpGet("pacjent/{pacjentId}")]
        public IActionResult getWizytyPacjenta(int pacjentId) 
        {
        var wizyty =_service.GetWizytyPacjenta(pacjentId).ToList();
        var result = wizyty.Select(w => map.WizytaToWidokDTO(w)).ToList();
            return Ok(result);
        }
        [HttpGet("anulowane")]
        public IActionResult GetAnulowane()
        {
            var wizyty = _service.GetWizytyAnulowane().ToList();
            var result = wizyty.Select(w => map.WizytaToWidokDTO(w)).ToList();
            return Ok(result);
        }


         [HttpGet("anulowane/lekarz/{lekarzId}")]
         public IActionResult GetAnulowaneByLekarz(int lekarzId)
         {
             var wizyty = _service.GetWizytyAnulowaneLekarza(lekarzId).ToList();
         var result = wizyty.Select(w => map.WizytaToWidokDTO(w)).ToList();
             return Ok(result);
         }
      


        [HttpPost("{id}/anuluj")]
        public async Task<IActionResult> AnulujWizyte(int id)
        {
            var result = await _service.AnulujWizyteAsync(id);
            if (!result) return NotFound("Nie znaleziono wizyty.");
            return Ok("Wizyta została anulowana.");
        }


    }
}