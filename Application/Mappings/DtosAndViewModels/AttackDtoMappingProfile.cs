using Application.Dtos.Attack;
using Application.ViewModels.Attack;
using AutoMapper;


namespace Application.Mappings.DtosAndViewModels;

public class AttackDtoMappingProfile : Profile
{
    public AttackDtoMappingProfile()
    {
        CreateMap<AttackDto, AttackViewModel>().ReverseMap();
    }
}