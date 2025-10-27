using System.Linq.Expressions;
using TecWebFest.Api.Entities;

namespace TecWebFest.Api.Repositories.Interfaces
{
    public interface IFestivalRepository
    {
        Task AddAsync(Festival festival);
        Task<Festival?> GetLineupAsync(int id);
        Task<int> SaveChangesAsync();
    }
}
