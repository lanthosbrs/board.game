using System.ComponentModel.DataAnnotations;

namespace board.game.GameDb.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? BggLink { get; set; }

        public int MinPlayers { get; set; } = 1;
        public int MaxPlayers { get; set; } = 1;
        public int? MinPlayTimeMinutes { get; set; }
        public int? MaxPlayTimeMinutes { get; set; }
        public int? MinAge { get; set; }
        public int? YearPublished { get; set; }
        public string? ThumbnailUrl { get; set; }

        /// <summary>Shelf number where the game is stored. For future WLED integration.</summary>
        public int? ShelfNumber { get; set; }

        public int? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        public ICollection<GameDesigner> GameDesigners { get; set; } = [];
        public ICollection<GameCategory> GameCategories { get; set; } = [];
        public ICollection<GameMechanic> GameMechanics { get; set; } = [];
    }
}
