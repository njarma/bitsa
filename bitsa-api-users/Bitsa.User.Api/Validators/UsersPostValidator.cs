using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Validators
{
    public class UsersPostValidator: AbstractValidator<UsersPostViewModel>
    {
        public UsersPostValidator()
        {
            RuleFor(x => x.Alias).NotEmpty().WithMessage("El alias es obligatorio");
            RuleFor(x => x.Email).EmailAddress().WithMessage("El formato del mail es incorrecto");
            RuleFor(x => x.Password).NotNull().Length(8, 50).WithMessage("La clave debe contener al menos 8 caracteres");
        }
    }
}
