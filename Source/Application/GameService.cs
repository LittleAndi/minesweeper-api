using Application;

namespace MineSweeper.Application;

public class GameService(IGameCreationService gameCreationService) : IGameService
{
    readonly Dictionary<Guid, Game> games = [];
    private readonly IGameCreationService gameCreationService = gameCreationService;

    public async Task<Game> StartGame(int boardSizeX, int boardSizeY, int mines)
    {
        return await Task.Run<Game>(async () =>
        {
            var game = await gameCreationService.CreateGame(boardSizeX, boardSizeY, mines);
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
