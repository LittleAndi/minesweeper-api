using MineSweeper.Domain;
using Xunit;
using Shouldly;

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
            board.ShouldNotBeNull();
            board.BoardSizeX.ShouldBe(boardSizeX);
            board.BoardSizeY.ShouldBe(boardSizeY);
            board.MineCount.ShouldBe(mines);
        }
    }
}