namespace board.game.GameDb.Models
{
    public class Designer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? BggLink { get; set; }

        public ICollection<GameDesigner> GameDesigners { get; set; } = [];
    }
}
