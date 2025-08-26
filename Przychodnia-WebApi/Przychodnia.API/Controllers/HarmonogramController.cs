using DTOs;
using IBLL;
using Microsoft.AspNetCore.Mvc;
using Models.Mapper;
using Models;
using BLL;

namespace Przychodnia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarmonogramController : ControllerBase
    {
        private readonly IHarmonogramService _harmonogramService;
       

        public HarmonogramController(IHarmonogramService harmonogramService)
        {
            _harmonogramService = harmonogramService;
            
        }

        [HttpGet]
          public ActionResult<IEnumerable<HarmonogramDTO>> GetAll()
         {
             var harmonogramy = _harmonogramService.PobierzWszystkie();
             return Ok(harmonogramy);
         }

        


        [HttpGet("Lekarz/{lekarzId}")]
        public ActionResult<IEnumerable<HarmonogramDTO>> GetByLekarzId(int lekarzId)
        {
            var harmonogramy = _harmonogramService.PobierzPoLekarzId(lekarzId);
            return Ok(harmonogramy);
        }


        [HttpGet("{id}")]
        public ActionResult<HarmonogramDTO> GetById(int id)
        {
            var harmonogram = _harmonogramService.PobierzPoId(id);
            if (harmonogram == null)
                return NotFound();

            return Ok(harmonogram);
        }




        [HttpPost]
        public ActionResult Create([FromBody] HarmonogramDTO harmonogramDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _harmonogramService.Dodaj(harmonogramDto);

            return Ok();
        }


        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] HarmonogramDTO harmonogramDto)
        {
           
            if (id != harmonogramDto.Id)
                return BadRequest("Id nie pasuje do obiektu");

            var istnieje = _harmonogramService.PobierzPoId(id);
            if (istnieje == null)
                return NotFound();

            _harmonogramService.Aktualizuj(harmonogramDto);

            return NoContent();
        }



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var istnieje = _harmonogramService.PobierzPoId(id);
            if (istnieje == null)
                return NotFound();

            _harmonogramService.Usun(id);

            return NoContent();
        }
    }
}
















