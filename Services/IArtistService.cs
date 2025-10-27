using TecWebFest.Api.DTOs;

namespace TecWebFest.Api.Services.Interfaces
{
    public interface IArtistService
    {
        Task<int> CreateAsync(CreateArtistDto dto);
        Task<ArtistScheduleDto?> GetScheduleAsync(int id);
    }
}
