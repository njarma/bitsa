using Bitsa.User.Api.ViewModels.ClassesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.ViewModels
{
    public class UsersGetViewModel: IdentificableViewModel
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Alias { get; set; }
        //public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Entry_Date { get; set; }
        public short Enabled { get; set; }
        public float Balance { get; set; }
        public short Administrator { get; set; }
    }
}
