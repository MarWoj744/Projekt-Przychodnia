using BLL;
using DTOs;
using IBLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IWizytaService _wizytaService;
        private readonly IPacjentService _pacjentService;
        private readonly PdfGeneratorService _pdfGenerator;
        private readonly Mapper map;

        public WykonaneBadaniaController(IWykonaneBadanieService service, IWizytaService wizytaService, IPacjentService pacjentService, PdfGeneratorService pdfGenerator)
        {
            _service = service;
            _wizytaService = wizytaService;
            _pacjentService = pacjentService;
            _pdfGenerator = pdfGenerator;
            map = new Mapper();
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
        [HttpGet("pobierz-pdf/{id}")]
        public IActionResult GenerujPdf(int id)
        {
            var badanie = _service.GetById(id);
            if (badanie == null)
                return NotFound($"Nie znaleziono wykonanego badania o ID {id}.");

           
            var wizyta = _wizytaService.GetWizytaById(badanie.WizytaId);
            if (wizyta == null)
                return BadRequest($"Nie znaleziono wizyty o ID {badanie.WizytaId}.");

            
            var pacjent = _pacjentService.GetPacjentById(wizyta.PacjentId);
            if (pacjent == null)
                return BadRequest($"Nie znaleziono pacjenta o ID {wizyta.PacjentId}.");

          
            var zalecenia = string.IsNullOrWhiteSpace(badanie.Zalecenia)
                ? "Brak zaleceń"
                : badanie.Zalecenia;

            var pdfBytes = _pdfGenerator.GeneratePrescriptionPdf(
                pacjent: $"{pacjent.Imie} {pacjent.Nazwisko}",
                zalecenia: zalecenia,
                qrText: $"BadanieID:{badanie.Id}"
            );

            return File(pdfBytes, "application/pdf", $"Recepta_{badanie.Id}.pdf");
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
