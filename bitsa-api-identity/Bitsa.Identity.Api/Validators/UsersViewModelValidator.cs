using FluentValidation;
using Bitsa.Identity.Api.ViewModels;

namespace Bitsa.Identity.Api.Validators
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            //e.g.
            //RuleFor(x => x.Id).NotNull();
            //RuleFor(x => x.Name).NotNull();
            //RuleFor(x => x.Name).NotEmpty();
            //RuleFor(x => x.Name).Length(0, 50);
            //RuleFor(x => x.Enabled).NotNull();
        }
    }
}
