using board.game.GameDb.Context;
using board.game.GameDb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace board.game.Services
{
    public class GameServices : IGameServices
    {
        private readonly GameDbContext _context;

        public GameServices(GameDbContext context)
        {
            _context = context;
        }

        public void AddGame(Game game)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _context.Games.ToList();

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
