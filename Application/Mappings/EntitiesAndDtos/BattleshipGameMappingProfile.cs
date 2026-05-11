using AutoMapper;
using Application.Dtos.BattleshipGame;
using Domain.Entities;

namespace Application.Mappings.EntitiesAndDtos;

public class BattleshipGameMappingProfile : Profile
{
    public BattleshipGameMappingProfile()
    {
        CreateMap<BattleshipGame, BattleshipGameDto>().ReverseMap();
    }
}