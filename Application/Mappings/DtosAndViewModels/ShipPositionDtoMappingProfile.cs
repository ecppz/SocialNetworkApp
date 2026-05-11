using Application.Dtos.ShipPosition;
using Application.ViewModels.ShipPosition;
using AutoMapper;

namespace Application.Mappings.DtosAndViewModels;

public class ShipPositionDtoMappingProfile : Profile
{
    public ShipPositionDtoMappingProfile()
    {
        CreateMap<ShipPositionDto, ShipPositionViewModel>().ReverseMap();
    }
}