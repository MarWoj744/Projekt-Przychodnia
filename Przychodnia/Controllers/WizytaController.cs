using Microsoft.AspNetCore.Mvc;
using Przychodnia.DTOs;
using Przychodnia.Services;
using System;
using System.Threading.Tasks;

namespace Przychodnia.Controllers
{
    [ApiController]
    [Route("api/wizyta")]
    public class WizytaController : ControllerBase
    {
        private readonly IWizytaService _service;

        public WizytaController(IWizytaService service)
        {
            _service = service;
        }

        [HttpPost("rejestruj")]
        public async Task<IActionResult> RejestrujWizyte([FromBody] RejestracjaWizytyDTO dto)
        {
            try
            {
                var result = await _service.ZarejestrujWizyteAsync(dto);
                return Ok("Wizyta zarejestrowana.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetWszystkie()
        {
            var wizyty = await _service.GetAllAsync();
            return Ok(wizyty);
        }

    }
}