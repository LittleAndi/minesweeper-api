namespace MineSweeper.Application;

public interface IGameService
{
    Task<Game> StartGame(int boardSizeX, int boardSizeY, int mines);
    Task<SquareInfo> RevealSquare(Guid gameId, int x, int y);
}
