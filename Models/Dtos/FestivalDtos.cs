using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TecWebFest.Api.DTOs
{
    public class CreateFestivalDto
    {
        [Required] public string Name { get; set; } = default!;
        [Required] public string City { get; set; } = default!;
        [Required] public DateTime StartDate { get; set; }
        [Required] public DateTime EndDate { get; set; }
        public List<CreateStageDto> Stages { get; set; } = new();
    }

    public class CreateStageDto
    {
        [Required] public string Name { get; set; } = default!;
    }

    public class FestivalLineupDto
    {
        public string Festival { get; set; } = default!;
        public string City { get; set; } = default!;
        public List<StageScheduleDto> Stages { get; set; } = new();
    }

    public class StageScheduleDto
    {
        public string Stage { get; set; } = default!;
        public List<PerformanceDto> Performances { get; set; } = new();
    }
}
