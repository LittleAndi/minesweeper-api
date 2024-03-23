namespace Test.Level0.Application;
[Trait("Category", "L0")]
public class BoardTests
{
    [Fact]
    public void ShouldGenerateNewTenByTenBoardWithTenMines()
    {
        var boardSizeX = 10;
        var boardSizeY = 10;
        var mines = 10;
        var board = new Board(boardSizeX, boardSizeY, mines, new RandomGenerator());
        board.ShouldNotBeNull();
        board.BoardSizeX.ShouldBe(boardSizeX);
        board.BoardSizeY.ShouldBe(boardSizeY);
        board.MineCount.ShouldBe(mines);
    }

    [Theory]
    // The first diagonal 10 squares should be mines
    [InlineData(0, 0, true, 1)]
    [InlineData(1, 1, true, 2)]
    [InlineData(2, 2, true, 2)]
    [InlineData(3, 3, true, 2)]
    [InlineData(4, 4, true, 2)]
    [InlineData(5, 5, true, 2)]
    [InlineData(6, 6, true, 2)]
    [InlineData(7, 7, true, 2)]
    [InlineData(8, 8, true, 2)]
    [InlineData(9, 9, true, 1)]

    // Line 0
    [InlineData(0, 1, false, 2)]
    [InlineData(0, 2, false, 1)]
    [InlineData(0, 3, false, 0)]
    [InlineData(0, 4, false, 0)]
    [InlineData(0, 5, false, 1)]
    [InlineData(0, 6, false, 2)]
    [InlineData(0, 7, false, 3)]
    [InlineData(0, 8, false, 2)]
    [InlineData(0, 9, false, 1)]

    // Line 1
    [InlineData(1, 0, false, 2)]
    [InlineData(1, 2, false, 2)]
    [InlineData(1, 3, false, 1)]
    [InlineData(1, 4, false, 0)]
    [InlineData(1, 5, false, 2)]
    [InlineData(1, 6, true, 3)]
    [InlineData(1, 7, true, 5)]
    [InlineData(1, 8, true, 3)]
    [InlineData(1, 9, false, 2)]

    // Line 2
    [InlineData(2, 0, false, 1)]
    [InlineData(2, 1, false, 2)]
    [InlineData(2, 3, false, 2)]
    [InlineData(2, 4, false, 1)]
    [InlineData(2, 5, false, 3)]
    [InlineData(2, 6, true, 5)]
    [InlineData(2, 7, true, 8)]
    [InlineData(2, 8, true, 5)]
    [InlineData(2, 9, false, 3)]

    // Line 3
    [InlineData(3, 0, false, 0)]
    [InlineData(3, 1, false, 1)]
    [InlineData(3, 2, false, 2)]
    [InlineData(3, 4, false, 2)]
    [InlineData(3, 5, false, 3)]
    [InlineData(3, 6, true, 3)]
    [InlineData(3, 7, true, 5)]
    [InlineData(3, 8, true, 3)]
    [InlineData(3, 9, false, 2)]
    public void ShouldRevealSquare(int x, int y, bool isMine, int adjacentMines)
    {
        var boardSizeX = 10;
        var boardSizeY = 10;
        var mines = 19;
        var board = new Board(boardSizeX, boardSizeY, mines, new NonRandomMinePlacementStrategy());
        var squareInfo = board.RevealSquare(x, y);
        squareInfo.ShouldNotBeNull();
        squareInfo.IsMine.ShouldBe(isMine);
        squareInfo.AdjacentMines.ShouldBe(adjacentMines);
    }
}