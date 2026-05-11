using Application.Dtos.Friend;
using Application.ViewModels.Friend;
using AutoMapper;

namespace Application.Mappings.DtosAndViewModels
{
    public class FriendDtoMappingProfile : Profile
    {
        public FriendDtoMappingProfile() 
        {
           
            CreateMap<FriendDto, FriendViewModel>().ReverseMap();
            CreateMap<FriendDto, DeleteFriendViewModel>();

        }
    }
}
