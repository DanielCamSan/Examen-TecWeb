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
            var startUtc = dto.StartTime.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc)
                : dto.StartTime.ToUniversalTime();

            var endUtc = dto.EndTime.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dto.EndTime, DateTimeKind.Utc)
                : dto.EndTime.ToUniversalTime();


            var artistExists = await _artists.ExistsAsync(dto.ArtistId);
            if (!artistExists)
                throw new KeyNotFoundException("Artist not found");

            var stageExists = await _stages.ExistsAsync(dto.StageId);
            if (!stageExists)
                throw new KeyNotFoundException("Stage not found");

            if (endUtc <= startUtc)
                throw new InvalidOperationException("End time must be greater than start time");


            var hasOverlap = await _performances.HasOverlapAsync(dto.StageId, startUtc, endUtc);
            if (hasOverlap)
                throw new InvalidOperationException("The stage already has a performance in this time range.");

            var performance = new Performance
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