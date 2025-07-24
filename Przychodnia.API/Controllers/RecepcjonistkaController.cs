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
    public class RecepcjonistkaController : ControllerBase
    {
        private readonly IRecepcjonistkaService _service;
        private readonly Mapper map;

        public RecepcjonistkaController(IRecepcjonistkaService service)
        {
            _service = service;
            map = new Mapper();
        }

        [HttpGet]
        [Authorize(Roles = "Recepcjonistka,Lekarz")]
        public IActionResult GetAll()
        {
            var recepcjonistki = _service.PobierzWszystkie();
            return Ok(recepcjonistki);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Recepcjonistka,Lekarz")]
        public IActionResult GetById(int id)
        {
            var recepcjonistka = _service.GetRecepcjonistkaById(id);
            if (recepcjonistka == null)
                return NotFound();

            return Ok(recepcjonistka);
        }

        [HttpPost]
        [Authorize(Roles = "Recepcjonistka")]
        public IActionResult Create([FromBody] RecepcjonistkaDTO recepcjonistka)
        {
            Recepcjonistka rec = map.RecepcjonistkaToEntity(recepcjonistka);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Dodaj(rec);
            _service.save();

            return CreatedAtAction(nameof(GetById), new { id = rec.Id }, rec);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Recepcjonistka")]
        public IActionResult Update(int id, [FromBody] RecepcjonistkaDTO recepcjonistka)
        {
            Recepcjonistka rec = map.RecepcjonistkaToEntity(recepcjonistka);
            if (id != recepcjonistka.Id)
                return BadRequest();

            _service.Update(rec);
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
    }
}