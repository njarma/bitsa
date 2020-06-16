using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bitsa.Base.Entites
{
    public class User
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        //[MinLength(1, ErrorMessage = "El alias es obligatorio")]
        public string Alias { get; set; }
        //[MinLength(8, ErrorMessage = "La clave debe contener al menos 8 caracteres")]
        public string Password { get; set; }
        //[EmailAddress(ErrorMessage = "El formato del mail ingresado es inválido")]
        public string Email { get; set; }
        public DateTime Entry_Date { get; set; }
        public Int16 Enabled { get; set; }
        public float Balance { get; set; }
        public Int16 Administrator { get; set; }
    }
}
