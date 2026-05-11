using Application.Dtos.Ship;
using AutoMapper;
using Domain.Entities;


namespace Application.Mappings.DtosAndEntities;

public class ShipMappingProfile : Profile
{
    public ShipMappingProfile()
    {
        CreateMap<Ship, ShipDto>().ReverseMap();
    }
}