using TecWebFest.Api.DTOs;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Services
{
    public class FestivalService : IFestivalService
    {
        //TODO INEYECCION DE DEPENDENCIAS
        private readonly IFestivalRepository _repo;
        public FestivalService(IFestivalRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> CreateFestivalAsync(CreateFestivalDto dto)
        {
            //TODO
            //pista para importar stages: Stages = dto.Stages.Select(s => new Stage { Name = s.Name }).ToList()
        }


        public async Task<FestivalLineupDto?> GetLineupAsync(int id)
        {
          //TODO
        }
    }
}
