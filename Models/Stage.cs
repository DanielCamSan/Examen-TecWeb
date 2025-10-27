using System.Collections.Generic;

namespace TecWebFest.Api.Entities
{
    public class Stage
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int FestivalId { get; set; }
        public Festival Festival { get; set; } = default!;

        // N:M Artists via Performance
        public ICollection<Performance> Performances { get; set; } = new List<Performance>();
    }
}
