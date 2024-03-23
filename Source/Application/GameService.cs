namespace MineSweeper.Application;

public class GameService : IGameService
{
    readonly Dictionary<Guid, Game> games = [];

    public async Task<Game> StartGame(int boardSizeX, int boardSizeY, int mines)
    {
        return await Task.Run<Game>(() =>
        {
            var game = new Game(boardSizeX, boardSizeY, mines);
            games.Add(game.GameId, game);
            return game;
        });
    }

    public Task<SquareInfo> RevealSquare(Guid gameId, int x, int y)
    {
        return Task.Run<SquareInfo>(() =>
        {
            if (!games.TryGetValue(gameId, out var game))
            {
                throw new Exception("Game not found");
            }
            var square = game.Board.RevealSquare(x, y);
            return square;
        });
    }

}
