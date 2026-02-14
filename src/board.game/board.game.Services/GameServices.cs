using board.game.GameDb.Context;
using board.game.GameDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _context.Games.ToList();
        }

        public Game GetGameById(int id)
        {
            var game = _context.Games.Find(id);
            if (game == null)
                throw new KeyNotFoundException($"Game with id {id} not found.");
            return game;
        }

        public Game GetGameByName(string name)
        {
            var game = _context.Games.FirstOrDefault(g => g.Name == name);
            if (game == null)
                throw new KeyNotFoundException($"Game with name '{name}' not found.");
            return game;
        }

        public void RemoveGame(Game game)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
        }

        public void UpdateGame(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }
    }
}
