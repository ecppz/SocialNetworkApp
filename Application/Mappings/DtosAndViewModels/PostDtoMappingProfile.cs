using Application.Dtos.Post;
using Application.ViewModels.Post;
using AutoMapper;

namespace Application.Mappings.DtosAndViewModels
{
    public class PostDtoMappingProfile : Profile
    {
        public PostDtoMappingProfile()
        {
            CreateMap<PostDto, PostViewModel>().ReverseMap();

            CreateMap<PostDto, SavePostViewModel>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImageFile))
                .ForMember(dest => dest.ImageFile, opt => opt.Ignore());

            CreateMap<PostDto, EditPostViewModel>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImageFile))
                .ForMember(dest => dest.ImageFile, opt => opt.Ignore());

            CreateMap<PostDto, DeletePostViewModel>();

            CreateMap<PostDto, PostDisplayDto>();

            CreateMap<PostDisplayDto, PostDisplayViewModel>().ReverseMap();
        }
    }

}
