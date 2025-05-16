using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Przychodnia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacjentController : ControllerBase
    {
        private readonly IPacjentService _service;

        public PacjentController(IPacjentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var pacjenci = _service.PobierzWszystkie();
            return Ok(pacjenci);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pacjent = _service.GetPacjentById(id);
            if (pacjent == null)
                return NotFound();

            return Ok(pacjent);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Pacjent pacjent)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validationResult = _service.ValidatePesel(pacjent);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            _service.Dodaj(pacjent);
            _service.save();

            return CreatedAtAction(nameof(GetById), new { id = pacjent.Id }, pacjent);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Pacjent pacjent)
        {
            if (id != pacjent.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validationResult = _service.ValidatePesel(pacjent);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            _service.Update(pacjent);
            _service.save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            _service.save();

            return NoContent();
        }
    }
}