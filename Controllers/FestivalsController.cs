using Microsoft.AspNetCore.Mvc;
using TecWebFest.Api.DTOs;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FestivalsController : ControllerBase
    {
        private readonly IFestivalService _service;

        public FestivalsController(IFestivalService service)
        {
            _service = service;
        }

        // POST: api/v1/festivals
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFestivalDto dto)
        {
            var id = await _service.CreateFestivalAsync(dto);
            return CreatedAtAction(nameof(GetLineup), new { id }, new { id });
        }

        // GET: api/v1/festivals/{id}/lineup
        [HttpGet("{id:int}/lineup")]
        public async Task<IActionResult> GetLineup([FromRoute] int id)
        {
            var data = await _service.GetLineupAsync(id);

            if (data == null) return NotFound();
            return Ok(data);
        }
    }
}
