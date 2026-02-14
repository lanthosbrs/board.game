using board.game.GameDb.Context;
using board.game.GameDb.Models;
using board.game.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace board.game.Tests.UnitTests;

[TestClass]
public class GameServicesTests
{
    private static GameDbContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<GameDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
        return new GameDbContext(options);
    }

    [TestMethod]
    public void GetAllGames_WhenEmpty_ReturnsEmptyList()
    {
        using var context = CreateContext(nameof(GetAllGames_WhenEmpty_ReturnsEmptyList));
        var sut = new GameServices(context);

        var result = sut.GetAllGames();

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    public void GetAllGames_WhenHasGames_ReturnsAllGames()
    {
        using var context = CreateContext(nameof(GetAllGames_WhenHasGames_ReturnsAllGames));
        context.Games.Add(new Game { Name = "Catan" });
        context.Games.Add(new Game { Name = "Ticket to Ride" });
        context.SaveChanges();

        var sut = new GameServices(context);
        var result = sut.GetAllGames().ToList();

        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(g => g.Name == "Catan"));
        Assert.IsTrue(result.Any(g => g.Name == "Ticket to Ride"));
    }

    [TestMethod]
    public void GetGameById_WhenExists_ReturnsGame()
    {
        using var context = CreateContext(nameof(GetGameById_WhenExists_ReturnsGame));
        context.Games.Add(new Game { Name = "Catan", MinPlayers = 2, MaxPlayers = 4 });
        context.SaveChanges();
        var expectedId = context.Games.Single(g => g.Name == "Catan").Id;

        var sut = new GameServices(context);
        var result = sut.GetGameById(expectedId);

        Assert.IsNotNull(result);
        Assert.AreEqual(expectedId, result.Id);
        Assert.AreEqual("Catan", result.Name);
        Assert.AreEqual(2, result.MinPlayers);
        Assert.AreEqual(4, result.MaxPlayers);
    }

    [TestMethod]
    public void GetGameById_WhenNotExists_ThrowsKeyNotFoundException()
    {
        using var context = CreateContext(nameof(GetGameById_WhenNotExists_ThrowsKeyNotFoundException));
        var sut = new GameServices(context);

        try
        {
            sut.GetGameById(999);
            Assert.Fail("Expected KeyNotFoundException");
        }
        catch (KeyNotFoundException) { }
    }

    [TestMethod]
    public void GetGameByName_WhenExists_ReturnsGame()
    {
        using var context = CreateContext(nameof(GetGameByName_WhenExists_ReturnsGame));
        context.Games.Add(new Game { Name = "Wingspan" });
        context.SaveChanges();

        var sut = new GameServices(context);
        var result = sut.GetGameByName("Wingspan");

        Assert.IsNotNull(result);
        Assert.AreEqual("Wingspan", result.Name);
    }

    [TestMethod]
    public void GetGameByName_WhenNotExists_ThrowsKeyNotFoundException()
    {
        using var context = CreateContext(nameof(GetGameByName_WhenNotExists_ThrowsKeyNotFoundException));
        var sut = new GameServices(context);

        try
        {
            sut.GetGameByName("NonExistent");
            Assert.Fail("Expected KeyNotFoundException");
        }
        catch (KeyNotFoundException) { }
    }

    [TestMethod]
    public void AddGame_PersistsGame()
    {
        using var context = CreateContext(nameof(AddGame_PersistsGame));
        var sut = new GameServices(context);
        var game = new Game { Name = "New Game", MinPlayers = 1, MaxPlayers = 2 };

        sut.AddGame(game);

        Assert.AreNotEqual(0, game.Id);
        var loaded = context.Games.Find(game.Id);
        Assert.IsNotNull(loaded);
        Assert.AreEqual("New Game", loaded.Name);
    }

    [TestMethod]
    public void UpdateGame_ModifiesExistingGame()
    {
        using var context = CreateContext(nameof(UpdateGame_ModifiesExistingGame));
        context.Games.Add(new Game { Name = "Original", MinPlayers = 2, MaxPlayers = 4 });
        context.SaveChanges();
        var game = context.Games.Single(g => g.Name == "Original");
        game.Name = "Updated";
        game.MinPlayers = 1;

        var sut = new GameServices(context);
        sut.UpdateGame(game);

        var loaded = context.Games.Find(game.Id);
        Assert.IsNotNull(loaded);
        Assert.AreEqual("Updated", loaded.Name);
        Assert.AreEqual(1, loaded.MinPlayers);
    }

    [TestMethod]
    public void RemoveGame_DeletesFromDatabase()
    {
        using var context = CreateContext(nameof(RemoveGame_DeletesFromDatabase));
        context.Games.Add(new Game { Name = "To Remove" });
        context.SaveChanges();
        var game = context.Games.Single(g => g.Name == "To Remove");

        var sut = new GameServices(context);
        sut.RemoveGame(game);

        Assert.IsNull(context.Games.Find(game.Id));
    }
}
