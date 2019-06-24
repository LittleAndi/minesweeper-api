namespace MineSweeper.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(GameDataContract), 201)]
        public async Task<IActionResult> CreateGame()
        {

        }
    }
}