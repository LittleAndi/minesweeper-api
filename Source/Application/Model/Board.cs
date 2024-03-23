using Application;

namespace MineSweeper.Domain;

public class Board
{
    private int boardSizeX;
    private int boardSizeY;
    private readonly IRandomGenerator randomGenerator;
    private int mines;
    private int[,] boardLayout;
    public int BoardSizeX => boardSizeX;
    public int BoardSizeY => boardSizeY;
    public int MineCount
    {
        get
        {
            int mineCount = 0;

            for (int y = 0; y < boardLayout.GetLength(1); y++)
            {
                for (int x = 0; x < boardLayout.GetLength(0); x++)
                {
                    if (boardLayout[x, y].Equals(9)) mineCount++;
                }
            }

            return mineCount;
        }
    }

    public Board(int boardSizeX, int boardSizeY, int mines, IRandomGenerator randomGenerator)
    {
        this.boardSizeX = boardSizeX;
        this.boardSizeY = boardSizeY;
        this.randomGenerator = randomGenerator;

        // Init board
        boardLayout = CreateLayout(mines);
    }

    private int[,] CreateLayout(int mines)
    {
        var layout = new int[boardSizeX, boardSizeY];

        for (int i = 0; i < mines; i++)
        {
            var mx = randomGenerator.Next(0, boardSizeX - 1);
            var my = randomGenerator.Next(0, boardSizeY - 1);

            while (layout[mx, my] == 9)
            {
                mx = randomGenerator.Next(0, boardSizeX - 1);
                my = randomGenerator.Next(0, boardSizeY - 1);
            }

            // Set mine
            layout[mx, my] = 9;
        }

        return layout;
    }

    public SquareInfo RevealSquare(int x, int y)
    {
        if (x < 0 || x >= boardSizeX || y < 0 || y >= boardSizeY)
        {
            throw new Exception("Invalid square");
        }

        // Check if square is a mine
        var isMine = boardLayout[x, y] == 9;

        // Find adjacent mines, don't count the square itself
        var adjacentMines = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                // Get the square coordiantes to test
                var testX = x + i;
                var testY = y + j;

                // Skip the square itself
                if (testX == x && testY == y) continue;

                if (testX >= 0 && testX < boardSizeX && testY >= 0 && testY < boardSizeY)
                {
                    if (boardLayout[testX, testY] == 9)
                    {
                        adjacentMines++;
                    }
                }
            }
        }

        var square = new SquareInfo
        {
            IsMine = isMine,
            IsRevealed = true,
            AdjacentMines = adjacentMines
        };

        return square;
    }
}