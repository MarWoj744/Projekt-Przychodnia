using DTOs;
using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models.Mapper;
using Models;

namespace Przychodnia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarmonogramController : ControllerBase
    {
        private readonly IHarmonogramService _harmonogramService;
        private readonly Mapper _map;

        public HarmonogramController(IHarmonogramService harmonogramService)
        {
            _harmonogramService = harmonogramService;
            _map = new Mapper();
        }

        [HttpGet]
        public ActionResult<IQueryable<Harmonogram>> GetAll()
        {
            var harmonogramy = _harmonogramService.PobierzWszystkie();
            return Ok(harmonogramy);
        }

        [HttpGet("Lekarz/{lekarzId}")]
        public ActionResult<IQueryable<Harmonogram>> GetByLekarzId(int lekarzId)
        {
            var harmonogramy = _harmonogramService.PobierzPoLekarzId(lekarzId);
            return Ok(harmonogramy);
        }

        [HttpGet("{id}")]
        public ActionResult<Harmonogram> GetById(int id)
        {
            var harmonogram = _harmonogramService.GetHarmonogramById(id);
            if (harmonogram == null)
                return NotFound();

            return Ok(harmonogram);
        }

        [HttpPost]
        public ActionResult Create([FromBody] HarmonogramDTO harmonogramDto)
        {
            Harmonogram harmonogram = _map.HarmonogramToEntity(harmonogramDto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _harmonogramService.Dodaj(harmonogram);
            _harmonogramService.Save();

            return CreatedAtAction(nameof(GetById), new { id = harmonogram.Id }, harmonogram);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] HarmonogramDTO harmonogramDto)
        {
            Harmonogram harmonogram = _map.HarmonogramToEntity(harmonogramDto);
            if (id != harmonogram.Id)
                return BadRequest("Id nie pasuje do obiektu");

            var istnieje = _harmonogramService.GetHarmonogramById(id);
            if (istnieje == null)
                return NotFound();

            _harmonogramService.Update(harmonogram);
            _harmonogramService.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var istnieje = _harmonogramService.GetHarmonogramById(id);
            if (istnieje == null)
                return NotFound();

            _harmonogramService.Delete(id);
            _harmonogramService.Save();

            return NoContent();
        }
    }
}
