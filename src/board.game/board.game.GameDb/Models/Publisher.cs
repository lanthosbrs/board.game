namespace board.game.GameDb.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Website { get; set; }
        public string? BggLink { get; set; }

        public ICollection<Game> Games { get; set; } = [];
    }
}
