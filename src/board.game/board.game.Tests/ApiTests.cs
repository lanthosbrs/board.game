using board.game.GameDb.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace board.game.Tests;

[TestClass]
public class ApiTests
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(60);

    private static async Task<(DistributedApplication app, CancellationToken ct)> StartAppAsync()
    {
        var cancellationToken = new CancellationTokenSource(DefaultTimeout).Token;
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.board_game_AppHost>(cancellationToken);
        appHost.Services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Warning);
            logging.AddFilter("Aspire.", LogLevel.Information);
        });
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        var app = await appHost.BuildAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);
        await app.StartAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);
        return (app, cancellationToken);
    }

    [TestMethod]
    public async Task Api_GetGames_ReturnsOkAndArray()
    {
        var (app, ct) = await StartAppAsync();
        await using (app)
        {
            var client = app.CreateHttpClient("api");
            await app.ResourceNotifications.WaitForResourceHealthyAsync("api", ct).WaitAsync(DefaultTimeout, ct);

            var response = await client.GetAsync("api/games", ct);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var games = await response.Content.ReadFromJsonAsync<List<Game>>(ct);
            Assert.IsNotNull(games);
        }
    }

    [TestMethod]
    public async Task Api_PostGame_ReturnsCreatedAndGetById_ReturnsGame()
    {
        var (app, ct) = await StartAppAsync();
        await using (app)
        {
            var client = app.CreateHttpClient("api");
            await app.ResourceNotifications.WaitForResourceHealthyAsync("api", ct).WaitAsync(DefaultTimeout, ct);

            var newGame = new Game { Name = "Test Game", MinPlayers = 2, MaxPlayers = 4 };
            var postResponse = await client.PostAsJsonAsync("api/games", newGame, ct);

            Assert.AreEqual(HttpStatusCode.Created, postResponse.StatusCode);
            var created = await postResponse.Content.ReadFromJsonAsync<Game>(ct);
            Assert.IsNotNull(created);
            Assert.IsTrue(created.Id > 0);
            Assert.AreEqual("Test Game", created.Name);

            var getResponse = await client.GetAsync($"api/games/{created.Id}", ct);
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
            var fetched = await getResponse.Content.ReadFromJsonAsync<Game>(ct);
            Assert.IsNotNull(fetched);
            Assert.AreEqual(created.Id, fetched.Id);
            Assert.AreEqual("Test Game", fetched.Name);
        }
    }

    [TestMethod]
    public async Task Api_GetGameById_WhenMissing_ReturnsNotFound()
    {
        var (app, ct) = await StartAppAsync();
        await using (app)
        {
            var client = app.CreateHttpClient("api");
            await app.ResourceNotifications.WaitForResourceHealthyAsync("api", ct).WaitAsync(DefaultTimeout, ct);

            var response = await client.GetAsync("api/games/99999", ct);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
