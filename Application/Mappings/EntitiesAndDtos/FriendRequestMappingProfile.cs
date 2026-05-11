using Application.Dtos.FriendRequest;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings.EntitiesAndDtos
{ 
    public class FriendRequestMappingProfile : Profile
    {
        public FriendRequestMappingProfile() {
           
            CreateMap<FriendRequest, FriendRequestDto>().ReverseMap();
            CreateMap<FriendRequest, FriendRequestDisplayDto>().ReverseMap();
        }
    }
}
