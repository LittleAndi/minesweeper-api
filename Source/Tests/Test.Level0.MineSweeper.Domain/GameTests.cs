using System;
using MineSweeper.Domain;
using Xunit;

namespace Test.Level0.MineSweeper.Domain
{
    [Trait("Category", "L0")]
    public class GameTests
    {
        [Fact]
        public void ShoudGenerateNewGameWithATenByTenBoardWithTenMines()
        {
            var boardSizeX = 10;
            var boardSizeY = 10;
            var mines = 10;
            var game = new Game(boardSizeX, boardSizeY, mines);
            Assert.NotNull(game);
            Assert.NotNull(game.Board);
            Assert.Equal(boardSizeX, game.Board.BoardSizeX);
            Assert.Equal(boardSizeY, game.Board.BoardSizeY);
            Assert.NotEqual(Guid.Empty, game.GameId);
        }
    }
}