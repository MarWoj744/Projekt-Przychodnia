using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadanieController : ControllerBase
    {
        private readonly IBadanieService _badanieService;

        public BadanieController(IBadanieService badanieService)
        {
            _badanieService = badanieService;
        }


        [HttpGet]
        public ActionResult<IQueryable<Badanie>> GetAll()
        {
            var badania = _badanieService.PobierzWszystkie();
            return Ok(badania);
        }


        [HttpGet("{id}")]
        public ActionResult<Badanie> GetById(int id)
        {
            var badanie = _badanieService.GetBadanieById(id);
            if (badanie == null)
                return NotFound();

            return Ok(badanie);
        }


        [HttpPost]
        public ActionResult Create([FromBody] Badanie badanie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _badanieService.Dodaj(badanie);
            _badanieService.save();

            return CreatedAtAction(nameof(GetById), new { id = badanie.Id }, badanie);
        }


        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Badanie badanie)
        {
            if (id != badanie.Id)
                return BadRequest("Id nie pasuje do obiektu");

            var istnieje = _badanieService.GetBadanieById(id);
            if (istnieje == null)
                return NotFound();

            _badanieService.Update(badanie);
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
