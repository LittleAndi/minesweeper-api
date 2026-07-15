using Microsoft.Extensions.Logging.Abstractions;

namespace Test.Level0.Application;

[Trait("Category", "L0")]
public class GameServiceTests
{
    // NonRandomMinePlacementStrategy places the first mine at (0,0),
    // so a 2x2 board with 1 mine has safe squares at (0,1), (1,0), (1,1)
    private static GameService CreateGameService() =>
        new(new NonRandomGameCreationService(), NullLogger<GameService>.Instance);

    [Fact]
    public async Task ShouldLoseGameWhenMineIsRevealed()
    {
        var gameService = CreateGameService();
        var game = await gameService.StartGame(2, 2, 1);

        var squareInfo = await gameService.RevealSquare(game.GameId, 0, 0);

        squareInfo.IsMine.ShouldBeTrue();
        gameService.GetGameStatus(game.GameId).ShouldBe(GameStatus.Lost);
    }

    [Fact]
    public async Task ShouldWinGameWhenAllSafeSquaresAreRevealed()
    {
        var gameService = CreateGameService();
        var game = await gameService.StartGame(2, 2, 1);

        await gameService.RevealSquare(game.GameId, 0, 1);
        await gameService.RevealSquare(game.GameId, 1, 0);
        gameService.GetGameStatus(game.GameId).ShouldBe(GameStatus.InProgress);

        await gameService.RevealSquare(game.GameId, 1, 1);
        gameService.GetGameStatus(game.GameId).ShouldBe(GameStatus.Won);
    }

    [Fact]
    public async Task ShouldRejectRevealOnLostGame()
    {
        var gameService = CreateGameService();
        var game = await gameService.StartGame(2, 2, 1);
        await gameService.RevealSquare(game.GameId, 0, 0);

        var exception = await Should.ThrowAsync<InvalidOperationException>(
            () => gameService.RevealSquare(game.GameId, 1, 1));
        exception.Message.ShouldBe("Game is already lost");
    }

    [Fact]
    public async Task ShouldRejectRevealOnWonGame()
    {
        var gameService = CreateGameService();
        var game = await gameService.StartGame(2, 2, 1);
        await gameService.RevealSquare(game.GameId, 0, 1);
        await gameService.RevealSquare(game.GameId, 1, 0);
        await gameService.RevealSquare(game.GameId, 1, 1);

        var exception = await Should.ThrowAsync<InvalidOperationException>(
            () => gameService.RevealSquare(game.GameId, 0, 1));
        exception.Message.ShouldBe("Game is already won");
    }
}
