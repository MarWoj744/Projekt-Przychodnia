using BLL;
using DTOs;
using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Mapper;
using System.Threading.Tasks;

namespace Przychodnia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WykonaneBadaniaController : Controller
    {
        private readonly IWykonaneBadanieService _service;
        private readonly Mapper map;

        public WykonaneBadaniaController(IWykonaneBadanieService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var wyniki = _service.GetAll();
            return Ok(wyniki);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var badanie = _service.GetById(id);
            if (badanie == null)
                return NotFound();

            return Ok(badanie);
        }

        [HttpPost]
        public IActionResult Create([FromBody] WykonaneBadaniaDTO badanieDto)
        {
            WykonaneBadania wykBad = map.WykonaneBadaniaToEntity(badanieDto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Dodaj(wykBad);
            _service.Save();
            return CreatedAtAction(nameof(GetById), new { id = wykBad.BadanieId }, wykBad);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] WykonaneBadaniaDTO badanieDto)
        {
            WykonaneBadania wykBad = map.WykonaneBadaniaToEntity(badanieDto);
            if (id != wykBad.BadanieId)
                return BadRequest();

            _service.Update(wykBad);
            _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            _service.Save();
            return NoContent();
        }
    }
}
