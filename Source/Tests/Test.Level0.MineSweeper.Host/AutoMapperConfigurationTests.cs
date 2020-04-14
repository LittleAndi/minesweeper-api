using System;
using MineSweeper.Domain;
using MineSweeper.Host;
using MineSweeper.Host.DataContracts;
using Xunit;
using Shouldly;

namespace Test.Level0.MineSweeper.Host
{
    [Trait("Category", "L0")]
    public class AutoMapperConfigurationTests
    {
        [Fact]
        public void AssertConfigurationIsValid()
        {
            AutoMapperConfiguration.Configure();
            AutoMapperConfiguration.Configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void ShoudMapGameToGameDataContract()
        {
            var boardSizeX = 10;
            var boardSizeY = 10;
            var mines = 10;

            AutoMapperConfiguration.Configure();
            var game = new Game(boardSizeX, boardSizeY, mines);
            var gameDataContract = AutoMapperConfiguration.Mapper.Map<GameDataContract>(game);
            gameDataContract.GameId.ShouldNotBe(Guid.Empty);
            gameDataContract.BoardSizeX.ShouldBe(boardSizeX);
            gameDataContract.BoardSizeY.ShouldBe(boardSizeY);
            gameDataContract.Mines.ShouldBe(mines);
        }
    }
}
