namespace Application;

public class GameNotFoundException(Guid gameId) : Exception($"Game {gameId} not found")
{
    public Guid GameId { get; } = gameId;
}

public class SquareOutOfBoundsException(int x, int y) : Exception($"Square ({x},{y}) is outside the board")
{
    public int X { get; } = x;
    public int Y { get; } = y;
}
