using System.Net.Http.Json;
using board.game.GameDb.Models;

namespace board.game.Web.Services;

public class GamesApiClient(HttpClient httpClient)
{
    public async Task<IReadOnlyList<Game>> GetGamesAsync(CancellationToken cancellationToken = default)
    {
        var games = await httpClient.GetFromJsonAsync<IReadOnlyList<Game>>("api/games", cancellationToken)
            ?? [];
        return games;
    }

    public async Task<Game?> GetGameByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"api/games/{id}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return null;
        return await response.Content.ReadFromJsonAsync<Game>(cancellationToken);
    }

    public async Task<Game?> CreateGameAsync(Game game, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/games", game, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return null;
        return await response.Content.ReadFromJsonAsync<Game>(cancellationToken);
    }

    public async Task<bool> UpdateGameAsync(int id, Game game, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"api/games/{id}", game, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteGameAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"api/games/{id}", cancellationToken);
        return response.IsSuccessStatusCode;
    }
}
