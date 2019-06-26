using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MineSweeper.Application;
using MineSweeper.Domain;
using MineSweeper.Host.DataContracts;

namespace MineSweeper.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;
        public GameController(IGameService gameService)
        {
            this.gameService = gameService;

            AutoMapperConfiguration.Configure();
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(GameDataContract), statusCode: 201)]
        public async Task<IActionResult> CreateGame([FromBody] InitGameDataContract gameParameters)
        {
            var game = await gameService.StartGame();
            var result = AutoMapperConfiguration.Mapper.Map<GameDataContract>(game);
            return Ok(result);
        }
    }
}