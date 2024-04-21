namespace Application;

/// <summary>
/// A service to manage all the running games.
/// </summary>
public interface IGameService
{
    Task<Game> StartGame(int boardSizeX, int boardSizeY, int mines);
    Task<SquareInfo> RevealSquare(Guid gameId, int x, int y);
    GameStatus GetGameStatus(Guid gameId);
}