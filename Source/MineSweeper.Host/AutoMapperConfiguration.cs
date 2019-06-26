using AutoMapper;
using MineSweeper.Domain;
using MineSweeper.Host.DataContracts;

namespace MineSweeper.Host
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration Configuration { get; set; }
        public static IMapper Mapper {get;set;}
        public static void Configure()
        {
            Configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<Game, GameDataContract>();
            });
            Mapper = Configuration.CreateMapper();
        }
    }

}