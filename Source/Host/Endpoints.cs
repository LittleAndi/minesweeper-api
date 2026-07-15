namespace Host.Endpoints;

public static class Endpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseRouting();

        var routeBuilder = app.MapGroup("/api");
        routeBuilder.MapPost("game", async (IGameService gameService, InitGameDto gameParameters) =>
        {
            var game = await gameService.StartGame(gameParameters.BoardSizeX, gameParameters.BoardSizeY, gameParameters.Mines);
            var result = GameMapper.MapGameToGameDto(game);
            return Results.Created(new Uri("http://localhost"), result);
        });

        // Add a put endpoint to reveal a square x,y, ie /api/game/{gameId}/1,2
        routeBuilder.MapPut("game/{gameId}/{x:int},{y:int}", async (IGameService gameService, Guid gameId, int x, int y) =>
        {
            try
            {
                // The finished-game check happens inside RevealSquare, atomically
                // with the reveal, so concurrent requests cannot slip past it
                var squareInfo = await gameService.RevealSquare(gameId, x, y);
                return Results.Ok(squareInfo);
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
    public int BoardSizeX { get; set; }
    public int BoardSizeY { get; set; }
    public int Mines { get; set; }
}

public class GameDto
{
    public Guid GameId { get; set; }
    public int BoardSizeX { get; set; }
    public int BoardSizeY { get; set; }
    public int Mines { get; set; }
}

public class FinishedGameDataContract : GameDto
{

}
