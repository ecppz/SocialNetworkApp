using Application.Dtos.BattleshipGame;
using Application.ViewModels.BattleshipGame;
using AutoMapper;

namespace Application.Mappings.DtosAndViewModels;

public class BattleshipGameDtoMappingProfile : Profile
{
    public BattleshipGameDtoMappingProfile()
    {
        CreateMap<BattleshipGameDto, BattleshipGameViewModel>().ReverseMap();
    }
}