using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TecWebFest.Api.Data;
using TecWebFest.Api.DTOs;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArtistsController : ControllerBase
    {
        //TODO INEYECCION DE DEPENDENCIAS - PISTA NECESITAS 2 INYECCIONES ARTIST Y PERFORMANCE
        public IArtistService _artist;
        public IPerformanceService _performance;

        public ArtistsController(IArtistService artistService,IPerformanceService performanceService)
        {
            _artist = artistService;
            _performance = performanceService;
        }
        // POST: api/v1/artists
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            int artistId = await _artist.CreateAsync(dto); 
            return CreatedAtAction(nameof(Create), artistId);
        }
        
        // GET: api/v1/artists/{id}/schedule
        [HttpGet("{id:int}/schedule")]
        public async Task<IActionResult> GetSchedule(int id)
        {
           //TODO CON MANEJO DE ERROR SI NO EXISTE EL ARTISTA
           var artist= await _artist.GetScheduleAsync(id);
           if(artist==null) throw new ArgumentNullException(nameof(artist));
           return Ok(artist);
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
