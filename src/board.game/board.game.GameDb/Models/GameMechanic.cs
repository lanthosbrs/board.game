namespace board.game.GameDb.Models
{
    public class GameMechanic
    {
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        public int MechanicId { get; set; }
        public Mechanic Mechanic { get; set; } = null!;
    }
}
