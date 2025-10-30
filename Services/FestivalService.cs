using TecWebFest.Api.DTOs;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories;
using TecWebFest.Api.Repositories.Interfaces;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Services
{
    public class FestivalService : IFestivalService
    {
        //TODO INEYECCION DE DEPENDENCIAS
        private readonly IFestivalRepository _repository;
        public FestivalService(IFestivalRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> CreateFestivalAsync(CreateFestivalDto dto)
        {
            //TODO: HECHO
            //pista para importar stages: Stages = dto.Stages.Select(s => new Stage { Name = s.Name }).ToList()
            var fest = new Festival
            {
                Name = dto.Name,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Stages = dto.Stages.Select(s => new Stage { Name = s.Name }).ToList()
            };
            await _repository.AddAsync(fest);
            await _repository.SaveChangesAsync();
            return fest.Id;
        }


        public async Task<FestivalLineupDto?> GetLineupAsync(int id)
        {
          //TODO
          var fest = await _repository.GetLineupAsync(id);
            if (fest == null) return null;
            return new FestivalLineupDto
            {
                Festival = fest.Name,
                City = fest.City,
                Stages = fest.Stages.Select(s => new StageScheduleDto
                {
                    Stage = s.Name,
                    Performances = s.Performances
                    .OrderBy(p => p.StartTime)
                    .Select(p => new PerformanceDto
                    {
                        ArtistId = p.ArtistId,
                        StartTime = p.StartTime,
                        EndTime = p.EndTime
                    }).ToList()
                }).ToList()
            };

        }
    }
}
