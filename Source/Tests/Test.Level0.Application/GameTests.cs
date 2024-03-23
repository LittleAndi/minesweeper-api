namespace Test.Level0.Application;
[Trait("Category", "L0")]
public class GameTests
{
    private static readonly IRandomGenerator nonRandomGameCreationService = new NonRandomMinePlacementStrategy();
    private static readonly IGameCreationService randomGameCreationService = new GameCreationService(new RandomGenerator());

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
