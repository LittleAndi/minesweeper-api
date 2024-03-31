namespace Test.Level0.MineSweeper.Host;

[Trait("Category", "L0")]
public class MappingProfileTests
{
    private readonly MapperConfiguration mapperConfiguration;
    private readonly IMapper mapper;
    readonly IGameCreationService gameCreationService = new GameCreationService(new RandomGenerator());

    public MappingProfileTests()
    {
        mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        mapper = mapperConfiguration.CreateMapper();
    }
    [Fact]
    public void AssertConfigurationIsValid()
    {
        mapperConfiguration.AssertConfigurationIsValid();
    }

    [Fact]
    public async Task ShoudMapGameToGameDataContract()
    {
        var boardSizeX = 10;
        var boardSizeY = 10;
        var mines = 10;

        var game = await gameCreationService.CreateGame(boardSizeX, boardSizeY, mines);
        var gameDataContract = mapper.Map<GameDto>(game);
        gameDataContract.GameId.ShouldNotBe(Guid.Empty);
        gameDataContract.BoardSizeX.ShouldBe(boardSizeX);
        gameDataContract.BoardSizeY.ShouldBe(boardSizeY);
        gameDataContract.Mines.ShouldBe(mines);
    }
}
