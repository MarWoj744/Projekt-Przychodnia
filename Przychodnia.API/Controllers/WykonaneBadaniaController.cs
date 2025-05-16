using BLL;
using DTOs;
using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace Przychodnia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WykonaneBadaniaController : Controller
    {
        private readonly IWykonaneBadanieService _service;

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Dodaj(badanieDto);
            _service.Save();
            return CreatedAtAction(nameof(GetById), new { id = badanieDto.BadanieId }, badanieDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] WykonaneBadaniaDTO badanieDto)
        {
            if (id != badanieDto.BadanieId)
                return BadRequest();

            _service.Update(badanieDto);
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
