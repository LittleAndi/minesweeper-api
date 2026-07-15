using System.Collections.Concurrent;

namespace Application;

public class GameService(IGameCreationService gameCreationService, ILogger<GameService> logger) : IGameService
{
    readonly ConcurrentDictionary<Guid, Game> games = new();
    private readonly IGameCreationService gameCreationService = gameCreationService;
    private readonly ILogger<GameService> logger = logger;

    public async Task<Game> StartGame(int boardSizeX, int boardSizeY, int mines)
    {
        var game = await gameCreationService.CreateGame(boardSizeX, boardSizeY, mines);
        games.TryAdd(game.GameId, game);

        // log board layout information
        var boardLayout = game.Board.GetBoardLayout();
        logger.LogInformation("BoardLayout: {boardLayout}", boardLayout);

        return game;
    }

    public Task<SquareInfo> RevealSquare(Guid gameId, int x, int y)
    {
        if (!games.TryGetValue(gameId, out var game))
        {
            throw new Exception("Game not found");
        }

        // Reveal and the resulting status transition must be atomic per game,
        // otherwise concurrent requests can reveal squares on a finished game
        lock (game.SyncRoot)
        {
            if (game.Status != GameStatus.InProgress)
            {
                throw new InvalidOperationException($"Game is already {(game.Status == GameStatus.Won ? "won" : "lost")}");
            }

            var square = game.Board.RevealSquare(x, y);

            if (square.IsMine)
            {
                game.Status = GameStatus.Lost;
            }
            else if (game.Board.BoardSizeX * game.Board.BoardSizeY - game.Board.MineCount == game.Board.RevealedCount)
            {
                game.Status = GameStatus.Won;
            }

            return Task.FromResult(square);
        }
    }

    public GameStatus GetGameStatus(Guid gameId)
    {
        if (!games.TryGetValue(gameId, out var game))
        {
            throw new Exception("Game not found");
        }
        return game.Status;
    }
}
