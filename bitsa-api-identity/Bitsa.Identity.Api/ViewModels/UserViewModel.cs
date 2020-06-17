using Bitsa.Identity.Api.ViewModels.ClassesBase;
using System;

namespace Bitsa.Identity.Api.ViewModels
{
    public class UserViewModel : IdentificableViewModel
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public DateTime Entry_Date { get; set; }
        public short Enabled { get; set; }
        public float Balance { get; set; }
        public short Administrator { get; set; }
        public string Alias { get; set; }
    }

    public class UserGetViewModel : UserViewModel
    {
        public Int16 Revoked { get; set; }
        public string Access_token { get; set; }
        public int Expires_in { get; set; }
    }

    public class UserPostViewModel : UserGetViewModel
    {
        public string Password { get; set; }
    }
}
