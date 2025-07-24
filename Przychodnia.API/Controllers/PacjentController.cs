using BLL;
using DTOs;
using IBLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Mapper;

namespace Przychodnia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacjentController : ControllerBase
    {
        private readonly IPacjentService _service;
        private readonly Mapper map;

        public PacjentController(IPacjentService service)
        {
            _service = service;
            map = new Mapper();
        }

        [HttpGet]
        [Authorize(Roles = "Recepcjonistka,Lekarz")]
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
        [Authorize(Roles = "Recepcjonistka")]
        public IActionResult Create([FromBody] PacjentDTO pacjent)
        {
            Pacjent pac = map.pacjentToEntity(pacjent);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validationResult = _service.ValidatePesel(pac);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            _service.Dodaj(pac);
            _service.save();

            return CreatedAtAction(nameof(GetById), new { id = pac.Id }, pac);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Recepcjonistka")]
        public IActionResult Update(int id, [FromBody] PacjentDTO pacjent)
        {
            Pacjent pac = map.pacjentToEntity(pacjent);
            if (id != pac.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validationResult = _service.ValidatePesel(pac);
            if (!string.IsNullOrEmpty(validationResult))
                return BadRequest(validationResult);

            _service.Update(pac);
            _service.save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Recepcjonistka")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            _service.save();

            return NoContent();
        }

        [HttpGet("historia-wizyt/{pacjentId}")]
        public IActionResult PobierzHistorieWizyt(int pacjentId)
        {
            var pdfBytes = _service.GenerujHistorieWizytPdf(pacjentId);
            return File(pdfBytes, "application/pdf", "historia_wizyt.pdf");
        }
    }
}