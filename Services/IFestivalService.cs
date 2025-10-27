using TecWebFest.Api.DTOs;
using TecWebFest.Api.Entities;

namespace TecWebFest.Api.Services.Interfaces
{
    public interface IFestivalService
    {
        Task<Festival> CreateFestivalAsync(CreateFestivalDto dto);
        Task<FestivalLineupDto?> GetLineupAsync(int id);
    }
}
