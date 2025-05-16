using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models;
using DTOs;
using System.Threading.Tasks;
using System.Linq;

namespace Przychodnia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WizytaController : ControllerBase
    {
        private readonly IWizytaService _service;

        public WizytaController(IWizytaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var wizyty = _service.GetAll();
            return Ok(wizyty);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var wizyta = _service.GetWizytaById(id);
            if (wizyta == null)
                return NotFound();

            return Ok(wizyta);
        }

        [HttpPost("zarejestruj")]
        public async Task<IActionResult> Register([FromBody] RejestracjaWizytyDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.ZarejestrujWizyteAsync(dto);
            if (!result)
                return BadRequest("Nie udało się zarejestrować wizyty.");

            return Ok("Wizyta zarejestrowana.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Wizyta wizyta)
        {
            if (id != wizyta.Id)
                return BadRequest();

            var result = await _service.UpdateWizytaAsync(wizyta);
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
    }
}