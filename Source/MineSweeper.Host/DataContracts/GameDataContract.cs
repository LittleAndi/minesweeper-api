using System;

namespace MineSweeper.Host.DataContracts
{
    public class GameDataContract
    {
        public Guid GameId { get; set; }
        public int BoardSizeX { get; }
        public int BoardSizeY { get; }
        public int Mines { get; }
    }
}