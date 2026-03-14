namespace Test.Level0.MineSweeper.Host;

[Trait("Category", "L0")]
public class MappingProfileTests
{
    readonly IGameCreationService gameCreationService = new GameCreationService(new RandomGenerator());

    [Fact]
    public async Task ShouldMapGameToGameDataContract()
    {
        var boardSizeX = 10;
        var boardSizeY = 10;
        var mines = 10;

        var game = await gameCreationService.CreateGame(boardSizeX, boardSizeY, mines);
        var gameDataContract = GameMapper.MapGameToGameDto(game);
        gameDataContract.GameId.ShouldNotBe(Guid.Empty);
        gameDataContract.BoardSizeX.ShouldBe(boardSizeX);
        gameDataContract.BoardSizeY.ShouldBe(boardSizeY);
        gameDataContract.Mines.ShouldBe(mines);
    }
}
