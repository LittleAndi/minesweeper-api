using System;
using MineSweeper.Domain;
using Xunit;
using Shouldly;

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
            game.ShouldNotBeNull();
            game.Board.ShouldNotBeNull();
            game.Board.BoardSizeX.ShouldBe(boardSizeX);
            game.Board.BoardSizeY.ShouldBe(boardSizeY);
            game.GameId.ShouldNotBe(Guid.Empty);
        }
    }
}