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
        // POST: api/v1/artist
        private readonly IArtistService _artistService;
        private readonly IPerformanceService _performance;

        public ArtistsController(IArtistService artistService, IPerformanceService performance)
        {
            _artistService = artistService;
            _performance = performance;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistDto dto)
        {
            return Ok(await _artistService.CreateAsync(dto));
        }

        // GET: api/v1/artists/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var data = await _artistService.GetScheduleAsync(id);
            return data is null ?NotFound():Ok();
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
