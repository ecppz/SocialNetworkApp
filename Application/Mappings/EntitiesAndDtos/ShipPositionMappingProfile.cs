using AutoMapper;
using Application.Dtos.ShipPosition;
using Domain.Entities;

namespace Application.Mappings.EntitiesAndDtos;

public class ShipPositionMappingProfile : Profile
{
    public ShipPositionMappingProfile()
    {
        CreateMap<ShipPosition, ShipPositionDto>().ReverseMap();
    }
}