using TecWebFest.Api.DTOs;

namespace TecWebFest.Api.Services.Interfaces
{
    public interface IFestivalService
    {
        Task<int> CreateFestivalAsync(CreateFestivalDto dto);
        Task<FestivalLineupDto?> GetLineupAsync(int id);
    }
}
