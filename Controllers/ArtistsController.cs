using Microsoft.AspNetCore.Mvc;
using TecWebFest.Api.DTOs;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artist;
        private readonly IPerformanceService _performance;

        public ArtistsController(IArtistService artist, IPerformanceService performance)
        {
            _artist = artist;
            _performance = performance;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistDto dto)
        {

            var id = await _artist.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSchedule), new { id }, new { id });
        }

        // GET: api/v1/artists/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var schedule = await _artist.GetScheduleAsync(id);
            if (schedule == null) return NotFound(new { error = $"Artist with ID {id} not found." });
            return Ok(schedule);
        }

        // POST: api/v1/artists/performances
        [HttpPost("performances")]
        public async Task<IActionResult> AddPerformance([FromBody] CreatePerformanceDto dto)
        {
            // --- INICIO SOLUCIÓN ---
            try
            {
                await _performance.AddPerformanceAsync(dto);
                return Ok(new { message = "Performance added successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            // --- FIN SOLUCIÓN ---
        }
    }
}