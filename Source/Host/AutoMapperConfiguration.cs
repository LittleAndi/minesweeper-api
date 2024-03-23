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
                config.CreateMap<Game, GameDataContract>()
                    .ForMember(dest => dest.BoardSizeX, opt => opt.MapFrom(src => src.Board.BoardSizeX))
                    .ForMember(dest => dest.BoardSizeY, opt => opt.MapFrom(src => src.Board.BoardSizeY))
                    .ForMember(dest => dest.Mines, opt => opt.MapFrom(src => src.Board.MineCount));
            });
            Mapper = Configuration.CreateMapper();
        }
    }

}