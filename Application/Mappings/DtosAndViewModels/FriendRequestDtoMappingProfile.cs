using Application.Dtos.FriendRequest;
using Application.ViewModels.FriendRequest;
using AutoMapper;

namespace Application.Mappings.DtosAndViewModels
{
    public class FriendRequestDtoMappingProfile : Profile
    {
        public FriendRequestDtoMappingProfile() 
        {
           
            CreateMap<FriendRequestDto, FriendRequestViewModel>().ReverseMap();

            CreateMap<FriendRequestDisplayDto, FriendRequestDisplayViewModel>().ReverseMap();

            CreateMap<FriendRequestFormDto, FriendRequestFormViewModel>().ReverseMap();

            CreateMap<FriendRequestListDto, FriendRequestListViewModel>().ReverseMap();

        }
    }
}
