using System.Collections.Generic;

namespace TecWebFest.Api.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string StageName { get; set; } = default!;
        public string Genre { get; set; } = default!;

        // N:M via Performance
        public ICollection<Performance> Performances { get; set; } = new List<Performance>();
    }
}
