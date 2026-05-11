using Application.Dtos.Attack;
using AutoMapper;
using Domain.Entities;


namespace Application.Mappings.EntitiesAndDtos;

public class AttackMappingProfile : Profile
{
    public AttackMappingProfile()
    {
        CreateMap<Attack, AttackDto>().ReverseMap();
    }
}