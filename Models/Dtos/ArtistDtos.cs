using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TecWebFest.Api.DTOs
{
    public class CreateArtistDto
    {
        [Required] public string StageName { get; set; } = default!;
        [Required] public string Genre { get; set; } = default!;
    }

    public class PerformanceDto
    {
        public int ArtistId { get; set; }
        public string Artist { get; set; } = default!;
        public int StageId { get; set; }
        public string Stage { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class CreatePerformanceDto
    {
        [Required] public int ArtistId { get; set; }
        [Required] public int StageId { get; set; }
        [Required] public DateTime StartTime { get; set; }
        [Required] public DateTime EndTime { get; set; }
    }

    public class ArtistScheduleDto
    {
        public string Artist { get; set; } = default!;
        public List<PerformanceDto> Slots { get; set; } = new();
    }
}
