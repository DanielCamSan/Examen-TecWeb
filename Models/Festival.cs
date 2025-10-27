using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TecWebFest.Api.Entities
{
    public class Festival
    {
        [Required] public int Id { get; set; }

        [Required] public string Name { get; set; } = default!;

        [Required] public string City { get; set; } = default!;
        [Required] public DateTime StartDate { get; set; }
        [Required] public DateTime EndDate { get; set; }
        //TODO relacion con stage
        public ICollection<Stage> Stages { get; set; } = new List<Stage>(); 
    }
}
