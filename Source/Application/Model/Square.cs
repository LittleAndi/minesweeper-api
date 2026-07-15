namespace Application.Model;

public class SquareInfo
{
    public bool IsMine { get; set; }
    public bool IsRevealed { get; set; }
    public int AdjacentMines { get; set; }
}

/// <summary>
/// The outcome of revealing a square, including the game status as it was
/// at the moment of the reveal (computed under the same lock).
/// </summary>
public record RevealResult(SquareInfo Square, GameStatus Status);
