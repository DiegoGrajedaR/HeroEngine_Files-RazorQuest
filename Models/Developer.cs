using System.ComponentModel.DataAnnotations;

namespace VideoGameManager.Models
{
    public class Developer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        [Range(1800, 2030)]
        public int FoundedYear { get; set; }

        // Propietat de navegació: un developer té molts jocs
        public ICollection<Game> Games { get; set; } = new List <Game>();
    }
}
