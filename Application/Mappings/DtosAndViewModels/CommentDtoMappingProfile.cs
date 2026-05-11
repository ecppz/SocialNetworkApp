using Application.Dtos.Comment;
using Application.ViewModels.Comment;
using AutoMapper;
namespace Application.Mappings.DtosAndViewModels
{ 
    public class CommentDtoMappingProfile : Profile
    {
        public CommentDtoMappingProfile() {
           
            CreateMap<CommentDto, CommentViewModel>().ReverseMap();
            CreateMap<CommentDto, CommentDisplayViewModel>()
                .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies))
                .ReverseMap();

            CreateMap<CommentDto, EditCommentViewModel>();
        }
    }
}
