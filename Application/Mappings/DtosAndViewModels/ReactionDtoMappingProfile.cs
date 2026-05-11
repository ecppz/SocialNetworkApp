using AutoMapper;
using Application.Dtos.Reaction;
using Application.ViewModels.Reaction;

namespace Application.Mappings.EntitiesAndDtos
{ 
    public class ReactionDtoMappingProfile : Profile
    {
        public ReactionDtoMappingProfile() {
           
            CreateMap<ReactionDto, ReactionViewModel>().ReverseMap();
        }
    }
}
