using AutoMapper;
using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.MappingServices
{
    public class UsersViewModelMappingService: Profile
    {
        public UsersViewModelMappingService() 
        {
            CreateMap<UsersViewModel, users>().ReverseMap();
            CreateMap<UsersGetViewModel, users>().ReverseMap();
            CreateMap<UsersGetViewModel, UsersViewModel>().ReverseMap();
            CreateMap<UsersGetViewModel, UsersViewModel>().ReverseMap();
            CreateMap<users, UsersPutViewModel>().ReverseMap()
                    .ForMember(x => x.Password, opt => opt.Ignore())
                    .ForMember(x => x.Balance, opt => opt.Ignore());
        }
    }
}
