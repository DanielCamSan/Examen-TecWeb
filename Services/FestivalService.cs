using System.Security.Cryptography.X509Certificates;
using TecWebFest.Api.Data;
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
        private readonly FestivalRepository _festivalRepository;


        public FestivalService(IFestivalRepository festivalRepositoy){
            _festivalRepository = festivalRepositoy;
        }

        public async Task<Festival> CreateFestivalAsync(CreateFestivalDto dto)
        {
            var fes = new Festival
            {
                Name = dto.Name,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Stages = dto.Stages.Select(s => new Stage { Name = s.Name }).ToList()
            };
            await _festivalRepository.AddAsync(fes);
            await _festivalRepository.SaveChangesAsync();
            return fes;
            //TODO
            //pista para importar stages: Stages = dto.Stages.Select(s => new Stage { Name = s.Name }).ToList()
        }


        public async Task<FestivalLineupDto?> GetLineupAsync(int id)
        {
          //TODO
        }
    }
}
