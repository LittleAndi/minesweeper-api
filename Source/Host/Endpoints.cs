using System;
using Microsoft.AspNetCore.Http;
using MineSweeper.Application;

namespace Host.Endpoints;

public static class Endpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseRouting();

        var routeBuilder = app.MapGroup("/api");
        routeBuilder.MapPost("game", async (IGameService gameService, InitGameDataContract gameParameters, IMapper mapper) =>
        {
            var game = await gameService.StartGame(gameParameters.BoardSizeX, gameParameters.BoardSizeY, gameParameters.Mines);
            var result = mapper.Map<GameDataContract>(game);
            return Results.Created(new Uri("http://localhost"), result);
        });

        // Add a put endpoint to reveal a square x,y, ie /api/game/{gameId}/1,2
        routeBuilder.MapPut("game/{gameId}/{x:int},{y:int}", async (IGameService gameService, Guid gameId, int x, int y, IMapper mapper) =>
        {
            var squareInfo = await gameService.RevealSquare(gameId, x, y);
            return Results.Ok(squareInfo);
        });


        return app;
    }
}