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
            if (!await _artists.ExistsAsync(dto.ArtistId))
                throw new KeyNotFoundException($"Artist with ID {dto.ArtistId} not found.");

            if (!await _stages.ExistsAsync(dto.StageId))
                throw new KeyNotFoundException($"Stage with ID {dto.StageId} not found.");

            if (dto.EndTime <= dto.StartTime)
                throw new InvalidOperationException("End time must be after start time.");

            bool overlaps = await _performances.HasOverlapAsync(dto.StageId, dto.StartTime, dto.EndTime);
            if (overlaps)
                throw new InvalidOperationException("The stage already has a performance in this time range.");

            var performance = new Performance
            {
                ArtistId = dto.ArtistId,
                StageId = dto.StageId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _performances.AddAsync(performance);
            await _performances.SaveChangesAsync();
        }
    }
}