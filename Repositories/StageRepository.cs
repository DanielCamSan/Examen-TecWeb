using Microsoft.EntityFrameworkCore;
using TecWebFest.Api.Data;

namespace TecWebFest.Repositories
{
    public class StageRepository : IStageRepository
    {
        // TODO INYECCION 

        public Task<bool> ExistsAsync(int id) =>
            _ctx.Stages.AnyAsync(s => s.Id == id);
    }
}
