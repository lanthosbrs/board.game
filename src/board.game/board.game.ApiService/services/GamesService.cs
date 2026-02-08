using Microsoft.AspNetCore.Mvc;

namespace board.game.ApiService.services
{
    [Route("api")]
    public class GamesService
    {

        public GamesService() { }

        public async Task<IActionResult> GetGamesAsync(CancellationToken cancellationToken = default)
        {
            return null;
        }
    }
}
