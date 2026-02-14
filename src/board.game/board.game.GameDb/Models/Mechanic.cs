namespace board.game.GameDb.Models
{
    /// <summary>
    /// Game mechanic (e.g. Worker Placement, Deck Building, Tile Placement).
    /// </summary>
    public class Mechanic
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? BggLink { get; set; }

        public ICollection<GameMechanic> GameMechanics { get; set; } = [];
    }
}
