using System;
using System.Collections.Generic;
using System.Text;

namespace board.game.GameDb.Models
{
    public class Game
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? BggLink { get; set; }


    }
}
