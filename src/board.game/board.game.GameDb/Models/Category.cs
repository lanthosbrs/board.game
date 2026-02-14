namespace board.game.GameDb.Models
{
    /// <summary>
    /// Game category or genre (e.g. Strategy, Party, Euro, Thematic).
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? BggLink { get; set; }

        public ICollection<GameCategory> GameCategories { get; set; } = [];
    }
}
