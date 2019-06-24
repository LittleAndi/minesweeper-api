using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MineSweeper.Host.DataContracts;

namespace MineSweeper.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(type: typeof(GameDataContract), statusCode: 201)]
        public async Task<IActionResult> CreateGame([FromBody] InitGameDataContract gameParameters)
        {
            throw new NotImplementedException();
        }
    }
}