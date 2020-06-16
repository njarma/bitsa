using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.ViewModels.ClassesBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Services.IServices
{
    public interface IGenericService<T> where T : BaseModel
    {
        public Task<IEnumerable<U>> GetAll<U>() where U : BaseViewModel;

        //public Task<IEnumerable<U>> GetFiltered(Expression<Func<U, bool>> condition);

        //public Task<U> GetSingle(Expression<Func<U, bool>> condition);

        public Task<U> Add<U>(U entity) where U : BaseViewModel;

        public Task<bool> Delete<U>(U entity) where U : BaseViewModel;

        public Task<bool> Update<U>(U entity) where U : BaseViewModel;

        //public Task<U> SaveEntity<U>(U entity) where U : BaseViewModel;
    }
}
