using board.game.GameDb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace board.game.Services
{
    public interface IGameServices
    {
        public Game GetGameById(int id);
        public Game GetGameByName(string name);
        public IEnumerable<Game> GetAllGames();
        public void AddGame(Game game);
        public void RemoveGame(Game game);
        public void UpdateGame(Game game);
        
    }
}
