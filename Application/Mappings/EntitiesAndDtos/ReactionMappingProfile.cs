using AutoMapper;
using Application.Dtos.Reaction;
using Domain.Entities;

namespace Application.Mappings.EntitiesAndDtos
{ 
    public class ReactionMappingProfile : Profile
    {
        public ReactionMappingProfile() {
           
            CreateMap<Reaction, ReactionDto>().ReverseMap();
        }
    }
}
