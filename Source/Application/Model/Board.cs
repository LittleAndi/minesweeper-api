using System.Text;

namespace Application.Model;

public class Board
{
    private readonly int boardSizeX;
    private readonly int boardSizeY;
    private readonly IRandomGenerator randomGenerator;
    private readonly bool[,] mineLayout;
    private readonly bool[,] revealedLayout;
    public int BoardSizeX => boardSizeX;
    public int BoardSizeY => boardSizeY;
    public int MineCount
    {
        get
        {
            int mineCount = 0;

            for (int y = 0; y < boardSizeY; y++)
            {
                for (int x = 0; x < boardSizeX; x++)
                {
                    if (mineLayout[x, y]) mineCount++;
                }
            }

            return mineCount;
        }
    }

    public int RevealedCount
    {
        get
        {
            int revealedCount = 0;

            for (int y = 0; y < boardSizeY; y++)
            {
                for (int x = 0; x < boardSizeX; x++)
                {
                    // Only safe squares count towards the win condition
                    if (revealedLayout[x, y] && !mineLayout[x, y]) revealedCount++;
                }
            }

            return revealedCount;
        }
    }

    public Board(int boardSizeX, int boardSizeY, int mines, IRandomGenerator randomGenerator)
    {
        if (boardSizeX < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(boardSizeX), boardSizeX, "Board must be at least 1 square wide");
        }
        if (boardSizeY < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(boardSizeY), boardSizeY, "Board must be at least 1 square high");
        }
        if (mines < 0 || mines >= boardSizeX * boardSizeY)
        {
            throw new ArgumentOutOfRangeException(nameof(mines), mines, "Mine count must leave at least one safe square");
        }

        this.boardSizeX = boardSizeX;
        this.boardSizeY = boardSizeY;
        this.randomGenerator = randomGenerator;

        // Init board
        mineLayout = CreateLayout(mines);
        revealedLayout = new bool[boardSizeX, boardSizeY];
    }

    private bool[,] CreateLayout(int mines)
    {
        var layout = new bool[boardSizeX, boardSizeY];

        for (int i = 0; i < mines; i++)
        {
            // Next is exclusive of the upper bound, so pass the board size itself
            var mx = randomGenerator.Next(0, boardSizeX);
            var my = randomGenerator.Next(0, boardSizeY);

            while (layout[mx, my])
            {
                mx = randomGenerator.Next(0, boardSizeX);
                my = randomGenerator.Next(0, boardSizeY);
            }

            // Set mine
            layout[mx, my] = true;
        }

        return layout;
    }

    public SquareInfo RevealSquare(int x, int y)
    {
        if (x < 0 || x >= boardSizeX || y < 0 || y >= boardSizeY)
        {
            throw new SquareOutOfBoundsException(x, y);
        }

        // Check if square is a mine
        var isMine = mineLayout[x, y];

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
                    if (mineLayout[testX, testY])
                    {
                        adjacentMines++;
                    }
                }
            }
        }

        // Set the square as revealed
        revealedLayout[x, y] = true;

        var square = new SquareInfo
        {
            IsMine = isMine,
            IsRevealed = true,
            AdjacentMines = adjacentMines
        };

        return square;
    }

    public string GetBoardLayout()
    {
        var sb = new StringBuilder();
        for (int y = 0; y < boardSizeY; y++)
        {
            for (int x = 0; x < boardSizeX; x++)
            {
                sb.Append(mineLayout[x, y] ? "M" : revealedLayout[x, y] ? "R" : ".");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}
