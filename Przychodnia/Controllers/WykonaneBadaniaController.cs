using Microsoft.AspNetCore.Mvc;
using Przychodnia.DTOs;
using Przychodnia.Services;
using System;
using System.Threading.Tasks;

namespace Przychodnia.Controllers
{
    [ApiController]
    [Route("api/wykonane-badania")]
    public class WykonaneBadanieController : ControllerBase
    {
        private readonly IWykonaneBadanieService _service;

        public WykonaneBadanieController(IWykonaneBadanieService service)
        {
            _service = service;
        }

        [HttpPost("dodaj")]
        public async Task<IActionResult> Dodaj([FromBody] WykonaneBadaniaDTO dto)
        {
            try
            {
                await _service.DodajWykonaneBadanieAsync(dto);
                return Ok("Badanie zosta³o dodane.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
