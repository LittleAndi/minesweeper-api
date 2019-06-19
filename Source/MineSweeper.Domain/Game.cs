using System;

namespace MineSweeper.Domain
{
    public class Game
    {
        public Guid GameId { get; private set; }
        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }
        public int Mines { get; set; }
        public Guid BoardId { get; private set; }

        public Board Board { get; set; }

        public Game(int boardSizeX, int boardSizeY, int mines)
        {
            BoardSizeX = boardSizeX;
            BoardSizeY = boardSizeY;
            Mines = mines;
        }
    }
}