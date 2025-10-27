using Microsoft.EntityFrameworkCore;
using TecWebFest.Api.Data;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;
using TecWebFest.Repositories;

namespace TecWebFest.Api.Repositories  
{
    public class StageRepository : IStageRepository
    {
        private readonly AppDbContext _ctx;
        public StageRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<bool> ExistsAsync(int id) =>
            _ctx.Stages.AnyAsync(s => s.Id == id);
    }
}