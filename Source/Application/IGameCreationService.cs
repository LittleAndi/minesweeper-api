namespace Application;

/// <summary>
/// A service to create new games.
/// </summary>
public interface IGameCreationService
{
    Task<Game> CreateGame(int boardSizeX, int boardSizeY, int mines);
}

public class GameCreationService(IRandomGenerator randomGenerator) : IGameCreationService
{
    private readonly IRandomGenerator randomGenerator = randomGenerator;

    public Task<Game> CreateGame(int boardSizeX, int boardSizeY, int mines)
    {
        var board = new Board(boardSizeX, boardSizeY, mines, randomGenerator);
        return Task.FromResult(new Game(board));
    }
}
