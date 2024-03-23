namespace MineSweeper.Domain;

public class Board
{
    private Random random;
    private int boardSizeX;
    private int boardSizeY;
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

    public Board(int boardSizeX, int boardSizeY, int mines)
    {
        random = new Random(DateTime.Now.Millisecond);

        this.boardSizeX = boardSizeX;
        this.boardSizeY = boardSizeY;

        // Init board
        boardLayout = CreateLayout(mines);
    }

    private int[,] CreateLayout(int mines)
    {
        var layout = new int[boardSizeX, boardSizeY];

        for (int i = 0; i < mines; i++)
        {
            var mx = random.Next(1, boardSizeX) - 1;
            var my = random.Next(1, boardSizeY) - 1;

            while (layout[mx, my] == 9)
            {
                mx = random.Next(1, boardSizeX) - 1;
                my = random.Next(1, boardSizeY) - 1;
            }

            // Set mine
            layout[mx, my] = 9;
        }

        return layout;
    }
}