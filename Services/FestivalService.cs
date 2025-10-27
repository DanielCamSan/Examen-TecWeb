using TecWebFest.Api.DTOs;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;
using TecWebFest.Api.Services.Interfaces;

public class FestivalService : IFestivalService
{
    private readonly IFestivalRepository _festivals;

    public FestivalService(IFestivalRepository festivals)
    {
        _festivals = festivals;
    }

    public async Task<int> CreateFestivalAsync(CreateFestivalDto dto)
    {
        if (dto.Stages == null || !dto.Stages.Any())
            throw new InvalidOperationException("Debes incluir al menos un stage.");

        if (dto.EndDate <= dto.StartDate)
            throw new InvalidOperationException("La fecha de fin debe ser posterior a la de inicio.");

        var festival = new Festival
        {
            Name = dto.Name,
            City = dto.City,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Stages = dto.Stages.Select(s => new Stage { Name = s.Name }).ToList()
        };

        await _festivals.AddAsync(festival);
        await _festivals.SaveChangesAsync();

        return festival.Id;
    }

    public async Task<FestivalLineupDto?> GetLineupAsync(int id)
    {
        var festival = await _festivals.GetLineupAsync(id);
        if (festival == null) return null;

        return new FestivalLineupDto
        {
            Festival = festival.Name,
            City = festival.City,
            Stages = festival.Stages
                .OrderBy(s => s.Name)
                .Select(s => new StageLineupDto
                {
                    Stage = s.Name,
                    Performances = s.Performances
                        .OrderBy(p => p.StartTime)
                        .Select(p => new PerformanceDto
                        {
                            ArtistId = p.ArtistId,
                            Artist = p.Artist.StageName,
                            StageId = s.Id,
                            Stage = s.Name,
                            StartTime = p.StartTime,
                            EndTime = p.EndTime
                        }).ToList()
                }).ToList()
        };
    }
}
