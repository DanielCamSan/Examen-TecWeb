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
        private readonly IArtistService _artist; 
        private readonly IPerformanceService _performance;
        public ArtistsController(IArtistService artistService, IPerformanceService performanceService)
        {
            _artist = artistService;
            _performance = performanceService;
        }
        // POST: api/v1/artists
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistDto dto)
        {
            //TODO
        }

        // GET: api/v1/artists/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
           //TODO CON MANEJO DE ERROR SI NO EXISTE EL ARTISTA
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
