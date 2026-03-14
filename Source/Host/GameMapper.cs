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
}
