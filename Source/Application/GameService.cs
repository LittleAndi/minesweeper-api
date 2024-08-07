namespace Application;

public class GameService(IGameCreationService gameCreationService, ILogger<GameService> logger) : IGameService
{
    readonly Dictionary<Guid, Game> games = [];
    private readonly IGameCreationService gameCreationService = gameCreationService;
    private readonly ILogger<GameService> logger = logger;

    public async Task<Game> StartGame(int boardSizeX, int boardSizeY, int mines)
    {
        return await Task.Run<Game>(async () =>
        {
            var game = await gameCreationService.CreateGame(boardSizeX, boardSizeY, mines);
            games.Add(game.GameId, game);

            // log board layout information
            var boardLayout = game.Board.GetBoardLayout();
            logger.LogInformation("BoardLayout: {boardLayout}", boardLayout);

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

            if (square.IsMine)
            {
                game.Status = GameStatus.Lost;
            }
            else if (game.Board.BoardSizeX * game.Board.BoardSizeY - game.Board.MineCount == game.Board.RevealedCount)
            {
                game.Status = GameStatus.Won;
            }

            return square;
        });
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
