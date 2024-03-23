namespace MineSweeper.Application;

public class GameService : IGameService
{
    Dictionary<Guid, Game> games = new Dictionary<Guid, Game>();

    public async Task<Game> StartGame(int boardSizeX, int boardSizeY, int mines)
    {
        return await Task.Run<Game>(() =>
        {
            var game = new Game(boardSizeX, boardSizeY, mines);
            games.Add(game.GameId, game);
            return game;
        });
    }
}
