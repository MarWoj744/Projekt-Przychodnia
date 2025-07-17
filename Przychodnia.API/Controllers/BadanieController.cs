using DTOs;
using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Mapper;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadanieController : ControllerBase
    {
        private readonly IBadanieService _badanieService;
        private readonly Mapper map;

        public BadanieController(IBadanieService badanieService)
        {
            _badanieService = badanieService;
        }


        [HttpGet]
        public ActionResult<IQueryable<WykonaneBadaniaDTO>> GetAll()
        {
            var badania = _badanieService.PobierzWszystkie();
            return Ok(badania);
        }


        [HttpGet("{id}")]
        public ActionResult<WykonaneBadaniaDTO> GetById(int id)
        {
            var badanie = _badanieService.GetBadanieById(id);
            if (badanie == null)
                return NotFound();

            return Ok(badanie);
        }


        [HttpPost]
        public ActionResult Create([FromBody] WykonaneBadaniaDTO badanie)
        {
            WykonaneBadania wykonaneBadania = map.WykonaneBadaniaToEntity(badanie);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _badanieService.Dodaj(wykonaneBadania);
            _badanieService.save();

            return CreatedAtAction(nameof(GetById), new { id = wykonaneBadania.BadanieId }, wykonaneBadania);
        }


        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] WykonaneBadaniaDTO badanie)
        {
            WykonaneBadania wykonaneBadania = map.WykonaneBadaniaToEntity(badanie);
            if (id != wykonaneBadania.BadanieId)
                return BadRequest("Id nie pasuje do obiektu");

            var istnieje = _badanieService.GetBadanieById(id);
            if (istnieje == null)
                return NotFound();

            _badanieService.Update(wykonaneBadania);
            _badanieService.save();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var istnieje = _badanieService.GetBadanieById(id);
            if (istnieje == null)
                return NotFound();

            _badanieService.Delete(id);
            _badanieService.save();

            return NoContent();
        }
    }
}
