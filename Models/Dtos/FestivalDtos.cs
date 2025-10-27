using System;
using System.Collections.Generic;

namespace TecWebFest.Api.DTOs
{
    public class CreateFestivalDto
    {
        public string Name { get; set; } = default!;
        public string City { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<StageDto> Stages { get; set; } = new();
    }

    public class StageDto
    {
        public string Name { get; set; } = default!;
    }

    public class FestivalLineupDto
    {
        public string Festival { get; set; } = default!;
        public string City { get; set; } = default!;
        public List<StageLineupDto> Stages { get; set; } = new();
    }

    public class StageLineupDto
    {
        public string Stage { get; set; } = default!;
        public List<PerformanceDto> Performances { get; set; } = new();
    }
}
