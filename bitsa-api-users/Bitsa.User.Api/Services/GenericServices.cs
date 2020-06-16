using Bitsa.User.Api.Exceptions.Bitsa;
using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.Repositories.IRepositories;
using Bitsa.User.Api.Services.IServices;
using Bitsa.User.Api.ViewModels.ClassesBase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Services
{
    public class GenericServices<T> : IGenericService<T> where T : BaseModel
    {
        protected IGenericRepository<T> _data;

        public GenericServices(IGenericRepository<T> data)
        {
            _data = data;
        }

        public virtual Task<IEnumerable<U>> GetAll<U>() where U : BaseViewModel
        {
            return _data.GetAll<U>();
        }

        //public virtual Task<IEnumerable<U>> GetFiltered(Expression<Func<U, bool>> condition)
        //{
        //    return _data.GetFiltered(condition);
        //}

        //public virtual Task<U> GetSingle(Expression<Func<U, bool>> condition)
        //{
        //    return _data.GetSingle(condition);
        //}

        public virtual Task<U> Add<U>(U entity) where U : BaseViewModel
        {
            return _data.Insert(entity).ContinueWith(t1 => { if (_data.SaveChanges().Result > 0) return t1.Result; else throw new BitsaInvalidOperationException(); });
        }

        public virtual Task<bool> Delete<U>(U entity) where U : BaseViewModel
        {
            return _data.Delete(entity).ContinueWith(p => { return _data.SaveChanges().Result > 0; });
        }

        public virtual Task<bool> Update<U>(U entity) where U : BaseViewModel
        {
            return _data.Update(entity).ContinueWith(p => { return _data.SaveChanges().Result > 0; });
        }

        //public virtual Task<U> SaveEntity<U>(U entity) where U : BaseViewModel
        //{
        //    throw new NotImplementedException();
        //}

    }
}
