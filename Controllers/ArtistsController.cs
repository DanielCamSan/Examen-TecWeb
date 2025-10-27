using Microsoft.AspNetCore.Mvc;
using TecWebFest.Api.DTOs;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IPerformanceService _performanceService;

        public ArtistsController(IArtistService artistService, IPerformanceService performanceService)
        {
            _artistService = artistService;
            _performanceService = performanceService;
        }

        // POST: api/v1/artists
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistDto dto)
        {
            var id = await _artistService.CreateAsync(dto);
            return Ok(new { id });
        }

        // GET: api/v1/artists/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var schedule = await _artistService.GetScheduleAsync(id);
            if (schedule == null)
                return NotFound(new { error = $"Artist with ID {id} not found." });

            return Ok(schedule);
        }

        // POST: api/v1/artists/performances
        [HttpPost("performances")]
        public async Task<IActionResult> AddPerformance([FromBody] CreatePerformanceDto dto)
        {
            try
            {
                await _performanceService.AddPerformanceAsync(dto);
                return Ok(new { message = "Performance added successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
