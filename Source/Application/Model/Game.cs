namespace MineSweeper.Domain;

public class Game(Board board)
{
    public Guid GameId { get; private set; } = Guid.NewGuid();
    public Board Board { get; set; } = board;
}