using TecWebFest.Api.DTOs;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Services
{
    public class FestivalService : IFestivalService
    {
        private readonly IFestivalRepository _repo;

        public FestivalService(IFestivalRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> CreateFestivalAsync(CreateFestivalDto dto)
        {
            var entity = new Festival
            {
                Name = dto.Name,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Stages = dto.Stages.Select(s => new Stage { Name = s.Name }).ToList()
            };
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return entity.Id;
        }


        public async Task<FestivalLineupDto?> GetLineupAsync(int id)
        {
            var entity = await _repo.GetLineupAsync(id);
            if(entity == null) return null;
            return new FestivalLineupDto
            {
                Festival = entity.Name,
                City = entity.City,
                Stages = entity.Stages.Select(s => new StageScheduleDto
                {
                    Stage = s.Name,
                    Performances = s.Performances
                        .Select(p => new PerformanceDto
                        {
                            ArtistId = p.ArtistId,
                            Artist = p.Artist.StageName,
                            StageId = p.StageId,
                            Stage = s.Name,
                            StartTime = p.StartTime,
                            EndTime = p.EndTime
                        }).ToList()
                }).ToList()
            };

        }
    }
}
