using TecWebFest.Api.DTOs;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Services
{
    public class FestivalService : IFestivalService
    {
        //TODO INEYECCION DE DEPENDENCIAS //HECHO
        private readonly IFestivalRepository _festival;

        public FestivalService(IFestivalRepository festival)
        {
            _festival = festival;
        }


        public async Task<int> CreateFestivalAsync(CreateFestivalDto dto)
        {
            //TODO
            //pista para importar stages: Stages = dto.Stages.Select(s => new Stage { Name = s.Name }).ToList()
            //HECHO
            var entity = new Festival { Stages = dto.Stages.Select(s => new Stage { Name = s.Name }).ToList()};
            await _festival.AddAsync(entity);
            await _festival.SaveChangesAsync();
            return entity.Id;
        }


        public async Task<FestivalLineupDto?> GetLineupAsync(int id)
        {
            var festival = await _festival.GetLineupAsync(id);
            if (festival == null) return null;

            return new FestivalLineupDto
            {
                Festival = festival.Name,
                Stages = festival.Stages
                .OrderBy(p => p.Id)
                .Select(p => new StageScheduleDto
            {}).ToList()
            };

        }
    }
}