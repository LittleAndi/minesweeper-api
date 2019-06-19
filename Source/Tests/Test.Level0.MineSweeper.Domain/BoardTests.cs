using MineSweeper.Domain;
using Xunit;

namespace Test.Level0.MineSweeper.Domain
{
    [Trait("Category", "L0")]
    public class BoardTests
    {
        [Fact]
        public void ShouldGenerateNewBoardWithTenMines()
        {
            var board = new Board(10, 10, 10);
            Assert.NotNull(board);
            Assert.Equal(10, board.MineCount);
        }
    }
}