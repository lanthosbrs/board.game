using Microsoft.Extensions.Logging;

namespace board.game.Tests;

[TestClass]
public class WebTests
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

    private static async Task<(DistributedApplication app, CancellationToken ct)> StartWebAppAsync()
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
    public async Task Web_GetRoot_ReturnsOk()
    {
        var (app, ct) = await StartWebAppAsync();
        await using (app)
        {
            var client = app.CreateHttpClient("webfrontend");
            await app.ResourceNotifications.WaitForResourceHealthyAsync("webfrontend", ct).WaitAsync(DefaultTimeout, ct);

            var response = await client.GetAsync("/", ct);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }

    [TestMethod]
    public async Task Web_GetGamesPage_ReturnsOk()
    {
        var (app, ct) = await StartWebAppAsync();
        await using (app)
        {
            var client = app.CreateHttpClient("webfrontend");
            await app.ResourceNotifications.WaitForResourceHealthyAsync("webfrontend", ct).WaitAsync(DefaultTimeout, ct);

            var response = await client.GetAsync("/games", ct);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }

    [TestMethod]
    public async Task Web_GetSettingsPage_ReturnsOk()
    {
        var (app, ct) = await StartWebAppAsync();
        await using (app)
        {
            var client = app.CreateHttpClient("webfrontend");
            await app.ResourceNotifications.WaitForResourceHealthyAsync("webfrontend", ct).WaitAsync(DefaultTimeout, ct);

            var response = await client.GetAsync("/settings", ct);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
