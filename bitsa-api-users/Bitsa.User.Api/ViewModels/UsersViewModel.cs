using Bitsa.User.Api.ViewModels.ClassesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.ViewModels
{
    public class UsersViewModel : UsersGetViewModel
    {
        public string Password { get; set; }
    }
}