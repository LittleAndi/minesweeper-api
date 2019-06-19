using MineSweeper.Domain;
using Xunit;

namespace Test.Level0.MineSweeper.Domain
{
    [Trait("Category", "L0")]
    public class GameTests
    {
        [Fact]
        public void ShoudGenerateNewGameWithATenByTenBoard()
        {
            var game = new Game(10, 10, 10);
            Assert.NotNull(game);
            Assert.NotNull(game.Board);
        }
    }
}