using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Przychodnia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecepcjonistkaController : ControllerBase
    {
        private readonly IRecepcjonistkaService _service;

        public RecepcjonistkaController(IRecepcjonistkaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var recepcjonistki = _service.PobierzWszystkie();
            return Ok(recepcjonistki);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var recepcjonistka = _service.GetRecepcjonistkaById(id);
            if (recepcjonistka == null)
                return NotFound();

            return Ok(recepcjonistka);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Recepcjonistka recepcjonistka)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Dodaj(recepcjonistka);
            _service.save();

            return CreatedAtAction(nameof(GetById), new { id = recepcjonistka.Id }, recepcjonistka);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Recepcjonistka recepcjonistka)
        {
            if (id != recepcjonistka.Id)
                return BadRequest();

            _service.Update(recepcjonistka);
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