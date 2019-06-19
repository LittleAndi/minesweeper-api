using System;

namespace MineSweeper.Domain
{
    public class Board
    {
        private int boardSizeX;
        private int boardSizeY;
        private int mines;

        public int MineCount => int.MinValue;

        public Board(int boardSizeX, int boardSizeY, int mines)
        {
            this.boardSizeX = boardSizeX;
            this.boardSizeY = boardSizeY;
            this.mines = mines;
        }
    }
}