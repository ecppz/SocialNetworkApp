using AutoMapper;
using Application.Dtos.Post;
using Domain.Entities;

namespace Application.Mappings.EntitiesAndDtos
{ 
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile() {
           
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Post, PostDisplayDto>();
        }
    }
}
