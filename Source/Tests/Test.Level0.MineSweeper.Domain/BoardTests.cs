using MineSweeper.Domain;
using Xunit;

namespace Test.Level0.MineSweeper.Domain
{
    [Trait("Category", "L0")]
    public class BoardTests
    {
        [Fact]
        public void ShouldGenerateNewTenByTenBoardWithTenMines()
        {
            var boardSizeX = 10;
            var boardSizeY = 10;
            var mines = 10;
            var board = new Board(boardSizeX, boardSizeY, mines);
            Assert.NotNull(board);
            Assert.Equal(boardSizeX, board.BoardSizeX);
            Assert.Equal(boardSizeY, board.BoardSizeY);
            Assert.Equal(mines, board.MineCount);
        }
    }
}