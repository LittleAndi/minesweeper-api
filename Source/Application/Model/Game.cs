namespace Application.Model;

public class Game(Board board)
{
    public Guid GameId { get; private set; } = Guid.NewGuid();
    public Board Board { get; set; } = board;
    public GameStatus Status { get; internal set; } = GameStatus.InProgress;
}

public enum GameStatus
{
    InProgress,
    Won,
    Lost
}