namespace board.game.GameDb.Models
{
    public class GameDesigner
    {
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        public int DesignerId { get; set; }
        public Designer Designer { get; set; } = null!;
    }
}
