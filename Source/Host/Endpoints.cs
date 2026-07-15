namespace Host.Endpoints;

public static class Endpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseRouting();

        var routeBuilder = app.MapGroup("/api");
        routeBuilder.MapPost("game", async (IGameService gameService, InitGameDto? gameParameters) =>
        {
            // No request body means default board parameters
            gameParameters ??= new InitGameDto();

            try
            {
                var game = await gameService.StartGame(gameParameters.BoardSizeX, gameParameters.BoardSizeY, gameParameters.Mines);
                var result = GameMapper.MapGameToGameDto(game);
                return Results.Created($"/api/game/{game.GameId}", result);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        // Add a put endpoint to reveal a square x,y, ie /api/game/{gameId}/1,2
        routeBuilder.MapPut("game/{gameId}/{x:int},{y:int}", async (IGameService gameService, Guid gameId, int x, int y) =>
        {
            try
            {
                // The finished-game check happens inside RevealSquare, atomically
                // with the reveal, so concurrent requests cannot slip past it
                var revealResult = await gameService.RevealSquare(gameId, x, y);
                return Results.Ok(GameMapper.MapRevealResultToDto(revealResult));
            }
            catch (GameNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (SquareOutOfBoundsException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });


        return app;
    }
}


public class InitGameDto
{
    public int BoardSizeX { get; set; } = 10;
    public int BoardSizeY { get; set; } = 10;
    public int Mines { get; set; } = 10;
}

public class GameDto
{
    public Guid GameId { get; set; }
    public int BoardSizeX { get; set; }
    public int BoardSizeY { get; set; }
    public int Mines { get; set; }
}

public class RevealSquareDto
{
    public string GameStatus { get; set; } = string.Empty;
    public bool IsMine { get; set; }
    public int AdjacentMines { get; set; }
}
