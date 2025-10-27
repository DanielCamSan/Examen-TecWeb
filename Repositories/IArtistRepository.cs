using TecWebFest.Api.Entities;

namespace TecWebFest.Api.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task AddAsync(Artist artist);
        Task<Artist?> GetScheduleAsync(int id);
        Task <Artist?> GetByIdAsync(int id);
        Task<int> SaveChangesAsync();
        Task<bool> ExistsAsync(int id);      
    }
}
