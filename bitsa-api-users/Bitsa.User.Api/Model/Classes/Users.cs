using System;
using System.Collections.Generic;

namespace Bitsa.User.Api.Model.Classes
{
    public partial class users
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Alias { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Entry_Date { get; set; }
        public short Enabled { get; set; }
        public float Balance { get; set; }
        public short Administrator { get; set; }
    }
}
