using Bitsa.User.Api.ViewModels.ClassesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.ViewModels
{
    public class UsersPutViewModel: IdentificableViewModel
    {
        public string Alias { get; set; }
        public float Balance { get; set; }
    }
}
