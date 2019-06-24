using System;
using System.Collections;
using System.Linq;

namespace MineSweeper.Domain
{
    public class Board
    {
        private Random random;
        private int boardSizeX;
        private int boardSizeY;
        private int mines;
        private int[,] boardLayout;

        public int MineCount
        {
            get
            {
                int mineCount = 0;

                for (int y = 0; y < this.boardLayout.GetLength(1); y++)
                {
                    for (int x = 0; x < this.boardLayout.GetLength(0); x++)
                    {
                        if (this.boardLayout[x, y].Equals(9)) mineCount++;
                    }
                }

                return mineCount;
            }
        }

        public Board(int boardSizeX, int boardSizeY, int mines)
        {
            this.random = new Random(DateTime.Now.Millisecond);

            this.boardSizeX = boardSizeX;
            this.boardSizeY = boardSizeY;
            this.mines = mines;

            boardLayout = CreateLayout();
        }

        private int[,] CreateLayout()
        {
            var layout = new int[this.boardSizeX, this.boardSizeY];

            return layout;
        }
    }
}