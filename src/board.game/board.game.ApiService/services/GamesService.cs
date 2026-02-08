using Microsoft.AspNetCore.Mvc;

namespace board.game.ApiService.services
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesService
    {

        public GamesService() { }

        [HttpGet]
        public async Task<IActionResult> GetGamesAsync(CancellationToken cancellationToken = default)
        {
            return null;
        }
    }
}
