using Microsoft.EntityFrameworkCore;
using TecWebFest.Api.Data;

namespace TecWebFest.Repositories
{
    public class StageRepository : IStageRepository
    {
        private readonly AppDbContext _ctx;

        public Task<bool> ExistsAsync(int id) =>
            _ctx.Stages.AnyAsync(s => s.Id == id);
    }
}
