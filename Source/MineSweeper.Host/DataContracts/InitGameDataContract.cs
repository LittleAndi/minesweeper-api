namespace MineSweeper.Host.DataContracts
{
    public class InitGameDataContract
    {
        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }
        public int Mines { get; set; }
    }
}