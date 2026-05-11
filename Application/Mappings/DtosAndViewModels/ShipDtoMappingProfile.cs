using Application.Dtos.Ship;
using Application.ViewModels.Ship;
using AutoMapper;

namespace Application.Mappings.DtosAndViewModels;

public class ShipDtopMappingProfile : Profile
{
    public ShipDtopMappingProfile()
    {
        CreateMap<ShipDto, ShipViewModel>().ReverseMap();
    }
}