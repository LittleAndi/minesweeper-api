namespace Host;

public static class GameMapper
{
    public static GameDto MapGameToGameDto(Game game)
    {
        return new GameDto
        {
            GameId = game.GameId,
            BoardSizeX = game.Board.BoardSizeX,
            BoardSizeY = game.Board.BoardSizeY,
            Mines = game.Board.MineCount
        };
    }

    public static RevealSquareDto MapRevealResultToDto(RevealResult revealResult)
    {
        return new RevealSquareDto
        {
            GameStatus = MapGameStatus(revealResult.Status),
            IsMine = revealResult.Square.IsMine,
            AdjacentMines = revealResult.Square.AdjacentMines
        };
    }

    public static string MapGameStatus(GameStatus status) => status switch
    {
        GameStatus.InProgress => "inProgress",
        GameStatus.Won => "won",
        GameStatus.Lost => "lost",
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unknown game status")
    };
}
