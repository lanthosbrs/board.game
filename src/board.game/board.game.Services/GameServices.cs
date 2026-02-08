using board.game.GameDb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace board.game.Services
{
    public class GameServices : IGameServices
    {
        private readonly IGameServices _gameService;

        public GameServices(IGameServices gameService)
        {
            _gameService = gameService;
        }

        public void AddGame(Game game)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> GetAllGames()
        {
            throw new NotImplementedException();
        }

        public Game GetGameById(int id)
        {
            throw new NotImplementedException();
        }

        public Game GetGameByName(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveGame(Game game)
        {
            throw new NotImplementedException();
        }

        public void UpdateGame(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
