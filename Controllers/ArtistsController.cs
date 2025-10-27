using Microsoft.AspNetCore.Mvc;
using TecWebFest.Api.DTOs;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArtistsController : ControllerBase
    {
        //TODO INEYECCION DE DEPENDENCIAS - PISTA NECESITAS 2 INYECCIONES ARTIST Y PERFORMANCE
        private readonly IArtistService _artists;

        public ArtistsController(IArtistService service)
        {
            _artists = service;
        }

        private readonly IPerformanceService _performance;
        public ArtistsController(IPerformanceService performance)
        {
            _performance = performance;
        }

        // POST: api/v1/artists
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistDto dto)
        {
            //TODO
            var id = await _artists.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSchedule), new { id }, new { id });
        }

        // GET: api/v1/artists/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
           //TODO CON MANEJO DE ERROR SI NO EXISTE EL ARTISTA
            var data = await _artists.GetScheduleAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
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
