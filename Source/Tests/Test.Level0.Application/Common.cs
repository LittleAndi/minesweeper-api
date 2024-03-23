namespace Test.Level0.Application;

internal class NonRandomGameCreationService : IGameCreationService
{
    public Task<Game> CreateGame(int boardSizeX, int boardSizeY, int mines)
    {
        var board = new Board(boardSizeX, boardSizeY, mines, new NonRandomMinePlacementStrategy());
        return Task.FromResult(new Game(board));
    }
}

internal class NonRandomMinePlacementStrategy : IRandomGenerator
{
    // This is a non-random implementation of IRandomGenerator
    // It will return the numbers between 1 and 9 in sequence

    // These "random" numbers should place mines in a diagonal line from top-left to bottom-right
    /*
    X 0 0 0 0 0 0 0 0 0
    0 X 0 0 0 0 X X X 0
    0 0 X 0 0 0 X X X 0
    0 0 0 X 0 0 X X X 0
    0 0 0 0 X 0 0 0 0 0
    0 0 0 0 0 X 0 0 0 0
    0 0 0 0 0 0 X 0 0 0
    0 0 0 0 0 0 0 X 0 0
    0 0 0 0 0 0 0 0 X 0
    0 0 0 0 0 0 0 0 0 X
    */
    private readonly int[] randomNumbers = [
        0,0,
        1,1,
        2,2,
        3,3,
        4,4,
        5,5,
        6,6,
        7,7,
        8,8,
        9,9,

        // Add a cluster of mines around 2,7 (9 extra stars)
        1,6,
        1,7,
        1,8,
        2,6,
        2,7,
        2,8,
        3,6,
        3,7,
        3,8,
    ];

    int index = 0;
    public int Next(int minValue, int maxValue)
    {
        return randomNumbers[index++];
    }
}