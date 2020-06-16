using System;

namespace Bitsa.User.Api.ViewModels.ClassesBase
{
    public class IdentificableViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            return obj is IdentificableViewModel model &&
                   Id == model.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
