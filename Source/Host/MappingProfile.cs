namespace Host;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Game, GameDto>()
             .ForMember(dest => dest.BoardSizeX, opt => opt.MapFrom(src => src.Board.BoardSizeX))
             .ForMember(dest => dest.BoardSizeY, opt => opt.MapFrom(src => src.Board.BoardSizeY))
             .ForMember(dest => dest.Mines, opt => opt.MapFrom(src => src.Board.MineCount));
    }
}