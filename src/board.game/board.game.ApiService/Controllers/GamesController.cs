using Microsoft.AspNetCore.Mvc;

namespace board.game.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController
    {

        [HttpGet]
        public async Task<IActionResult> GetGames(CancellationToken cancellationToken = default)
        {
            return null;
        }
    }
}
