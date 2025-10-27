using TecWebFest.Api.DTOs;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;
using TecWebFest.Api.Services.Interfaces;
using TecWebFest.Repositories;

namespace TecWebFest.Api.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IPerformanceRepository _performances;
        private readonly IStageRepository _stages;
        private readonly IArtistRepository _artists;

        public PerformanceService(
            IPerformanceRepository performances,
            IStageRepository stages,
            IArtistRepository artists)
        {
            _performances = performances;
            _stages = stages;
            _artists = artists;
        }

        public async Task AddPerformanceAsync(CreatePerformanceDto dto)
        {

            // PISTA TE PASO COMO EXTRAER START TIME Y END TIME EN FORMATO UTC
            var startUtc = dto.StartTime.Kind == DateTimeKind.Unspecified
               ? DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc)
               : dto.StartTime.ToUniversalTime();

           var endUtc = dto.EndTime.Kind == DateTimeKind.Unspecified
               ? DateTime.SpecifyKind(dto.EndTime, DateTimeKind.Utc)
               : dto.EndTime.ToUniversalTime();
            var artist = await _artists.ExistsAsync(dto.ArtistId);
            var stage = await _stages.ExistsAsync(dto.StageId);
            if(!artist) throw new Exception("Artist doesnt exist");
            if(!stage) throw new Exception("Stage doesnt exist");
            if (endUtc <= startUtc) throw new Exception("End time must be greater than star time");
            var aviable =await _performances.HasOverlapAsync(dto.StageId, startUtc, endUtc);
            if (!aviable)
            {
                throw new Exception("The stage is not available in the given time range");
            }
            var performance= new Performance
            {
                ArtistId = dto.ArtistId,
                StageId = dto.StageId,
                StartTime = startUtc,
                EndTime = endUtc
            };
            await _performances.AddAsync(performance);
            await _performances.SaveChangesAsync();

        }
    }
}
