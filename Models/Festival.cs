using System.Collections.Generic;

namespace TecWebFest.Api.Entities
{
    public class Festival
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string City { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //1 -> N

        public ICollection<Stage> stages { get; set; } = new List<Stage>();
    }
}
