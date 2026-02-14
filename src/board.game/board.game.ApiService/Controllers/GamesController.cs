using Microsoft.AspNetCore.Mvc;
using board.game.GameDb.Models;
using board.game.Services;

namespace board.game.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameServices _gameServices;

        public GamesController(IGameServices gameServices)
        {
            _gameServices = gameServices;
        }

        /// <summary>Get all games.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Game>), StatusCodes.Status200OK)]
        public IActionResult GetGames(CancellationToken cancellationToken = default)
        {
            var games = _gameServices.GetAllGames();
            return Ok(games);
        }

        /// <summary>Get a game by id.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGameById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var game = _gameServices.GetGameById(id);
                return Ok(game);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>Get a game by name.</summary>
        [HttpGet("by-name/{name}")]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGameByName(string name, CancellationToken cancellationToken = default)
        {
            try
            {
                var game = _gameServices.GetGameByName(name);
                return Ok(game);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>Create a new game.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(Game), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddGame([FromBody] Game game, CancellationToken cancellationToken = default)
        {
            if (game == null)
                return BadRequest();
            _gameServices.AddGame(game);
            return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
        }

        /// <summary>Update an existing game.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateGame(int id, [FromBody] Game game, CancellationToken cancellationToken = default)
        {
            if (game == null)
                return BadRequest();
            try
            {
                var existing = _gameServices.GetGameById(id);
                existing.Name = game.Name;
                existing.Description = game.Description;
                existing.BggLink = game.BggLink;
                existing.MinPlayers = game.MinPlayers;
                existing.MaxPlayers = game.MaxPlayers;
                existing.MinPlayTimeMinutes = game.MinPlayTimeMinutes;
                existing.MaxPlayTimeMinutes = game.MaxPlayTimeMinutes;
                existing.MinAge = game.MinAge;
                existing.YearPublished = game.YearPublished;
                existing.ThumbnailUrl = game.ThumbnailUrl;
                existing.PublisherId = game.PublisherId;
                _gameServices.UpdateGame(existing);
                return Ok(existing);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>Delete a game.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteGame(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var game = _gameServices.GetGameById(id);
                _gameServices.RemoveGame(game);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
