using TecWebFest.Api.DTOs;

namespace TecWebFest.Api.Services.Interfaces
{
    public interface IPerformanceService
    {
        Task AddPerformanceAsync(CreatePerformanceDto dto);
    }
}
