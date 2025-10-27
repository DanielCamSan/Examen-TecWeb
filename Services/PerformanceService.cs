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
            // Convertir a UTC
            var startUtc = dto.StartTime.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc)
                : dto.StartTime.ToUniversalTime();

            var endUtc = dto.EndTime.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dto.EndTime, DateTimeKind.Utc)
                : dto.EndTime.ToUniversalTime();

            // Validar que exista el artista
            if (!await _artists.ExistsAsync(dto.ArtistId))
                throw new InvalidOperationException($"Artist with ID {dto.ArtistId} does not exist.");

            // Validar que exista el stage
            if (!await _stages.ExistsAsync(dto.StageId))
                throw new InvalidOperationException($"Stage with ID {dto.StageId} does not exist.");

            // Validar que la fecha de fin sea mayor que la de inicio
            if (endUtc <= startUtc)
                throw new InvalidOperationException("End time must be after start time.");

            // Validar que no haya solapamiento
            if (await _performances.HasOverlapAsync(dto.StageId, startUtc, endUtc))
                throw new InvalidOperationException("The stage already has a performance in this time range.");

            // Crear performance
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
