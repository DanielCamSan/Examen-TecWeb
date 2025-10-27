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
            //var startUtc = dto.StartTime.Kind == DateTimeKind.Unspecified
            //    ? DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc)
            //    : dto.StartTime.ToUniversalTime();

            //var endUtc = dto.EndTime.Kind == DateTimeKind.Unspecified
            //    ? DateTime.SpecifyKind(dto.EndTime, DateTimeKind.Utc)
            //    : dto.EndTime.ToUniversalTime();

            // TODO VERIFICAR QUE EXISTA ARTISTA, STAGES Y QUE LA FECHA DE FIN SEA MAYOR QUE LA DEL INICIO


            // TODO VERIFICAR QUE NO HAYA SOLAPAMIENTO ENTRE PERFEROMANCES VER QUE EL STAGE ESTE LIBRE.
           
            // ANADIR EL PERFORMANCE
            var startUtc = dto.StartTime.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc)
                : dto.StartTime.ToUniversalTime();
            var endUtc = dto.EndTime.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dto.EndTime, DateTimeKind.Utc)
                : dto.EndTime.ToUniversalTime();
            Artist? artist = await _artists.GetScheduleAsync(dto.ArtistId);
            bool stage = await _stages.ExistsAsync(dto.StageId)
            if (artist == null || stage == false || endUtc <= startUtc)
            {
                throw new ArgumentException("Invalid artist, stage, or time range.");
            }
            bool isOverlapping = await _performances.HasOverlapAsync(dto.StageId, startUtc, endUtc);
            if (isOverlapping)
            {
                throw new InvalidOperationException("The performance time overlaps with an existing performance on the same stage.");
            }
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
