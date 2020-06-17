using AutoMapper;
using Bitsa.Identity.Api.Model.Classes;
using Bitsa.Identity.Api.ViewModels;

namespace Bitsa.Identity.Api.MappingServices
{
    public class UserViewModelMappingService : Profile
    {
        public UserViewModelMappingService()
        {
            CreateMap<users, UserViewModel>().ReverseMap();
            CreateMap<users, UserGetViewModel>().ReverseMap();
            CreateMap<users, UserPostViewModel>().ReverseMap();
            CreateMap<UserPostViewModel, UserGetViewModel>().ReverseMap();
        }
    }
}
