using TecWebFest.Api.Entities;

namespace TecWebFest.Repositories
{
    public interface IPerformanceRepository
    {
        Task AddAsync(Performance performance);
        Task<bool> HasOverlapAsync(int stageId, DateTime start, DateTime end);
        Task<int> SaveChangesAsync();
    }
}
