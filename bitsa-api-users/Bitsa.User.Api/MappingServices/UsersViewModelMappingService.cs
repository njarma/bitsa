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
            CreateMap<UsersGetViewModel, users>().ReverseMap();
        }
    }
}
