namespace Test.Level0.MineSweeper.Host;

[Trait("Category", "L0")]
public class AutoMapperConfigurationTests
{
    private readonly MapperConfiguration mapperConfiguration;
    private readonly IMapper mapper;

    public AutoMapperConfigurationTests()
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
    public void ShoudMapGameToGameDataContract()
    {
        var boardSizeX = 10;
        var boardSizeY = 10;
        var mines = 10;

        var game = new Game(boardSizeX, boardSizeY, mines);
        var gameDataContract = mapper.Map<GameDataContract>(game);
        gameDataContract.GameId.ShouldNotBe(Guid.Empty);
        gameDataContract.BoardSizeX.ShouldBe(boardSizeX);
        gameDataContract.BoardSizeY.ShouldBe(boardSizeY);
        gameDataContract.Mines.ShouldBe(mines);
    }
}
