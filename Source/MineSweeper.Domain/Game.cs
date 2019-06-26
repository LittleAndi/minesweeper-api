using System;

namespace MineSweeper.Domain
{
    public class Game
    {
        public Guid GameId { get; private set; }
        public Board Board { get; set; }

        public Game(int boardSizeX, int boardSizeY, int mines)
        {
            Board = new Board(boardSizeX, boardSizeY, mines);
            GameId = new Guid();
        }
    }
}