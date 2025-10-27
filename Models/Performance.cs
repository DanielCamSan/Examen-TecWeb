namespace TecWebFest.Api.Entities
{
    // Join entity for N:M between Artist and Stage, with payload (date/time).
    public class Performance
    {
        public int ArtistId { get; set; }
        public Artist Artist { get; set; } = default!;

        public int StageId { get; set; }
        public Stage Stage { get; set; } = default!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


    }
}

