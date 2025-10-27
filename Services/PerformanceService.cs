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

            var artist = await _artists.GetByIdAsync(dto.ArtistId);
            if (artist == null)
                throw new InvalidOperationException($"No se encontró el artista con ID {dto.ArtistId}.");

            var stage = await _stages.GetByIdAsync(dto.StageId);
            if (stage == null)
                throw new InvalidOperationException($"No se encontró el stage con ID {dto.StageId}.");

            if (endUtc <= startUtc)
                throw new InvalidOperationException("La hora de finalización debe ser posterior a la de inicio.");

            var existingPerformances = await _performances.GetByStageIdAsync(dto.StageId);
            bool overlaps = existingPerformances.Any(p =>
                (startUtc >= p.StartTime && startUtc < p.EndTime) ||
                (endUtc > p.StartTime && endUtc <= p.EndTime) ||
                (startUtc <= p.StartTime && endUtc >= p.EndTime)
            );

            if (overlaps)
                throw new InvalidOperationException("Ya existe una presentación en ese escenario en el horario indicado.");

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
