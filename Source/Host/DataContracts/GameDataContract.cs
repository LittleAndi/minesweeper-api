using System;

namespace MineSweeper.Host.DataContracts
{
    public class GameDataContract
    {
        public Guid GameId { get; set; }
        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }
        public int Mines { get; set; }
    }
}