using TecWebFest.Api.DTOs;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artists;

        public ArtistService(IArtistRepository artists)
        {
            _artists = artists;
        }

        public async Task<int> CreateAsync(CreateArtistDto dto)
        {
            var entity = new Artist { StageName = dto.StageName, Genre = dto.Genre };
            await _artists.AddAsync(entity);
            await _artists.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<ArtistScheduleDto?> GetScheduleAsync(int id)
        {
            var artist = await _artists.GetScheduleAsync(id);
            if (artist == null) return null;

            return new ArtistScheduleDto
            {
                Artist = artist.StageName,
                Slots = artist.Performances
                    .OrderBy(p => p.StartTime)
                    .Select(p => new PerformanceDto
                    {
                        ArtistId = artist.Id,
                        Artist = artist.StageName,
                        StageId = p.StageId,
                        Stage = p.Stage.Name,
                        StartTime = p.StartTime,
                        EndTime = p.EndTime
                    }).ToList()
            };
        }
    }
}