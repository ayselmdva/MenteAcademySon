using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationBD.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Profession? Profession { get; set; }
        public int ProfessionId { get; set; }
        [NotMapped]
        public IFormFile ?formFile { get; set; }
        public string ?Image { get; set; }
    }
}
