using System.Threading.Tasks;
using Application;
using Microsoft.VisualBasic;
using MineSweeper.Application;

namespace Test.Level0.MineSweeper.Domain;
[Trait("Category", "L0")]
public class GameTests
{
    private static readonly IRandomGenerator nonRandomGameCreationService = new NonRandomMinePlacementStrategy();
    private static readonly IGameCreationService randomGameCreationService = new GameCreationService(nonRandomGameCreationService);

    [Fact]
    public async Task ShoudGenerateNewGameWithATenByTenBoardWithTenMines()
    {
        var boardSizeX = 10;
        var boardSizeY = 10;
        var mines = 10;
        var game = await randomGameCreationService.CreateGame(boardSizeX, boardSizeY, mines);
        game.ShouldNotBeNull();
        game.Board.ShouldNotBeNull();
        game.Board.BoardSizeX.ShouldBe(boardSizeX);
        game.Board.BoardSizeY.ShouldBe(boardSizeY);
        game.GameId.ShouldNotBe(Guid.Empty);
    }
}

internal class NonRandomGameCreationService : IGameCreationService
{
    public Task<Game> CreateGame(int boardSizeX, int boardSizeY, int mines)
    {
        var board = new Board(boardSizeX, boardSizeY, mines, new NonRandomMinePlacementStrategy());
        return Task.FromResult(new Game(board));
    }
}

internal class NonRandomMinePlacementStrategy : IRandomGenerator
{
    // This is a non-random implementation of IRandomGenerator
    // It will return the numbers between 1 and 9 in sequence

    // These "random" numbers should place mines in a diagonal line from top-left to bottom-right
    private readonly int[] randomNumbers = [
        1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,
    ];

    int index = 0;
    public int Next(int minValue, int maxValue)
    {
        return randomNumbers[index++];
    }
}