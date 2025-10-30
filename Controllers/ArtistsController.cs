using Microsoft.AspNetCore.Mvc;
using TecWebFest.Api.DTOs;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArtistsController : ControllerBase
    {
        //TODO INEYECCION DE DEPENDENCIAS - PISTA NECESITAS 2 INYECCIONES ARTIST Y PERFORMANCE : HECHO
        private readonly IArtistService _artist;
        private readonly IPerformanceService _performance;
        public ArtistsController(IArtistService artist, IPerformanceService performance)
        {
            _artist = artist;
            _performance = performance;
        }

        // POST: api/v1/artists
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var artist = await _artist.CreateAsync(dto);
            return CreatedAtAction(nameof(Create), artist);
        }

        // GET: api/v1/artists/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var schedule = await _artist.GetScheduleAsync(id);
            if(schedule == null)
            {
                return NotFound(new { error = "Artist Schedule Not Found", status = 404 });
            }
            return Ok(schedule);

        }

        // POST: api/v1/artists/performances
        [HttpPost("performances")]
        public async Task<IActionResult> AddPerformance([FromBody] CreatePerformanceDto dto)
        {
            await _performance.AddPerformanceAsync(dto);
            return Ok();
        }
    }
}
