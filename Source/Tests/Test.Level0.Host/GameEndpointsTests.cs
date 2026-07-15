using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace Test.Level0.MineSweeper.Host;

/// <summary>
/// Places mines in row-major order: mine k lands on cell (k % sizeX, k / sizeX).
/// Cells are always distinct, so the board's retry loop never spins, and the
/// first mine is always at (0,0). Stateful: use a fresh instance per game.
/// </summary>
internal class RowMajorMinePlacer : IRandomGenerator
{
    private int sizeX;
    private int cell;
    private bool expectingX = true;

    public int Next(int minValue, int maxValue)
    {
        // Board placement calls alternate x (max = sizeX) and y (max = sizeY)
        if (expectingX)
        {
            expectingX = false;
            sizeX = maxValue;
            return cell % sizeX;
        }
        expectingX = true;
        var y = cell / sizeX % maxValue;
        cell++;
        return y;
    }
}

public class DeterministicGameApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IRandomGenerator, RowMajorMinePlacer>();
        });
    }
}

[Trait("Category", "L0")]
public class GameEndpointsTests : IDisposable
{
    private readonly DeterministicGameApiFactory factory;
    private readonly HttpClient client;

    public GameEndpointsTests()
    {
        // The mine placer is stateful (it walks the board in row-major order),
        // so every test gets its own factory to keep placement predictable
        factory = new DeterministicGameApiFactory();
        client = factory.CreateClient();
    }

    public void Dispose()
    {
        factory.Dispose();
        GC.SuppressFinalize(this);
    }

    private async Task<Guid> CreateSingleMineGame()
    {
        // With LowerBoundRandomGenerator the mine is always at (0,0)
        var response = await client.PostAsJsonAsync("/api/game", new InitGameDto { BoardSizeX = 2, BoardSizeY = 2, Mines = 1 });
        var game = await response.Content.ReadFromJsonAsync<GameDto>();
        return game!.GameId;
    }

    [Fact]
    public async Task ShouldCreateDefaultGameWhenBodyIsEmpty()
    {
        var response = await client.PostAsync("/api/game", null);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        var game = await response.Content.ReadFromJsonAsync<GameDto>();
        game.ShouldNotBeNull();
        game.BoardSizeX.ShouldBe(10);
        game.BoardSizeY.ShouldBe(10);
        game.Mines.ShouldBe(10);
        response.Headers.Location.ShouldNotBeNull();
        response.Headers.Location.OriginalString.ShouldBe($"/api/game/{game.GameId}");
    }

    [Fact]
    public async Task ShouldRejectBoardWithTooManyMines()
    {
        var response = await client.PostAsJsonAsync("/api/game", new InitGameDto { BoardSizeX = 2, BoardSizeY = 2, Mines = 4 });
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnNotFoundForUnknownGame()
    {
        var response = await client.PutAsync($"/api/game/{Guid.NewGuid()}/1,1", null);
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldReturnNotFoundForSquareOutsideBoard()
    {
        var gameId = await CreateSingleMineGame();
        var response = await client.PutAsync($"/api/game/{gameId}/5,5", null);
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldLoseGameAndRejectFurtherReveals()
    {
        var gameId = await CreateSingleMineGame();

        var response = await client.PutAsync($"/api/game/{gameId}/0,0", null);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var reveal = await response.Content.ReadFromJsonAsync<RevealSquareDto>();
        reveal.ShouldNotBeNull();
        reveal.IsMine.ShouldBeTrue();
        reveal.GameStatus.ShouldBe("lost");

        var afterLoss = await client.PutAsync($"/api/game/{gameId}/1,1", null);
        afterLoss.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldWinGameWhenAllSafeSquaresAreRevealed()
    {
        var gameId = await CreateSingleMineGame();

        var first = await client.PutAsync($"/api/game/{gameId}/0,1", null);
        var firstReveal = await first.Content.ReadFromJsonAsync<RevealSquareDto>();
        firstReveal!.GameStatus.ShouldBe("inProgress");
        firstReveal.IsMine.ShouldBeFalse();
        firstReveal.AdjacentMines.ShouldBe(1);

        await client.PutAsync($"/api/game/{gameId}/1,0", null);
        var last = await client.PutAsync($"/api/game/{gameId}/1,1", null);
        var lastReveal = await last.Content.ReadFromJsonAsync<RevealSquareDto>();
        lastReveal!.GameStatus.ShouldBe("won");
    }
}
