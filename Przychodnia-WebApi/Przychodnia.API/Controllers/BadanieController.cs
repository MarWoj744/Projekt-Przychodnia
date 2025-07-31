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
            map = new Mapper();
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
        public ActionResult Create([FromBody] BadanieDTO badanie)
        {
            Badanie badanie1 = map.BadanieToEntity(badanie);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _badanieService.Dodaj(badanie1);
            _badanieService.save();

            return CreatedAtAction(nameof(GetById), new { id = badanie1.Id }, badanie1);
        }


        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] BadanieDTO badanie)
        {
            Badanie badanie1 = map.BadanieToEntity(badanie);
            if (id != badanie1.Id)
                return BadRequest("Id nie pasuje do obiektu");

            var istnieje = _badanieService.GetBadanieById(id);
            if (istnieje == null)
                return NotFound();

            _badanieService.Update(badanie1);
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
