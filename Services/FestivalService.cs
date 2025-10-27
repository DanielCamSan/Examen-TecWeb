using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security;
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

            var entity = new Festival { 
                Name= dto.Name,
                City=dto.City,
                EndDate=dto.EndDate,
                StartDate=dto.StartDate,
                Stages= dto.Stages.Select(s => new Stage { Name = s.Name }).ToList()
            };




            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return entity.Id;
        }


        public async Task<FestivalLineupDto?> GetLineupAsync(int id)
        {
            //TODO
            var Festival=await _repo.GetLineupAsync(id);
            if (Festival == null) throw new Exception("Festival no existe");
            var LineUp = new FestivalLineupDto();
            LineUp.Festival = Festival.Name;
            LineUp.City=Festival.City;
            foreach (var stage in Festival.Stages)
            {
                StageScheduleDto temp = new StageScheduleDto();
                temp.Stage = stage.Name;
                
                foreach(var performance in stage.Performances)
                {
                    PerformanceDto tempPerformance = new PerformanceDto();
                    tempPerformance.Artist = performance.Artist.StageName;
                    tempPerformance.ArtistId = performance.ArtistId;
                    tempPerformance.StageId= performance.StageId;
                    tempPerformance.Stage = performance.Stage.Name;
                    tempPerformance.StartTime=performance.StartTime;
                    tempPerformance.EndTime=performance.EndTime;
                    temp.Performances.Add(tempPerformance);
                }
                LineUp.Stages.Add(temp);
            }
            return LineUp;
        }
    }
}
