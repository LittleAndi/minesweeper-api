using System;

namespace MineSweeper.Domain
{
    public class Board
    {
        private Random random;
        private int boardSizeX;
        private int boardSizeY;
        private int mines;
        private int[,] boardLayout;

        public int MineCount => int.MinValue;

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