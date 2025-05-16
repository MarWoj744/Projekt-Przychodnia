using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Przychodnia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LekarzController : ControllerBase
    {
        private readonly ILekarzService _lekarzService;

        public LekarzController(ILekarzService lekarzService)
        {
            _lekarzService = lekarzService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var lekarze = _lekarzService.PobierzWszystkie().ToList();
            return Ok(lekarze);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var lekarz = _lekarzService.GetLekarzById(id);
            if (lekarz == null)
                return NotFound();

            return Ok(lekarz);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Lekarz lekarz)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _lekarzService.Dodaj(lekarz);
            _lekarzService.save();

            return CreatedAtAction(nameof(GetById), new { id = lekarz.Id }, lekarz);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Lekarz lekarz)
        {
            if (id != lekarz.Id)
                return BadRequest("ID w URL i obiekcie się różnią");

            var istnieje = _lekarzService.GetLekarzById(id);
            if (istnieje == null)
                return NotFound();

            _lekarzService.Update(lekarz);
            _lekarzService.save();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var lekarz = _lekarzService.GetLekarzById(id);
            if (lekarz == null)
                return NotFound();

            _lekarzService.Delete(id);
            _lekarzService.save();

            return NoContent();
        }
    }
}
