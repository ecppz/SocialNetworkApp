using Application.Dtos.Comment;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings.EntitiesAndDtos
{ 
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile() {
           
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
