namespace Application.Model;

public class Game(Board board)
{
    public Guid GameId { get; private set; } = Guid.NewGuid();
    public Board Board { get; set; } = board;
    public GameStatus Status { get; internal set; } = GameStatus.InProgress;
    internal object SyncRoot { get; } = new();
}

public enum GameStatus
{
    InProgress,
    Won,
    Lost
}