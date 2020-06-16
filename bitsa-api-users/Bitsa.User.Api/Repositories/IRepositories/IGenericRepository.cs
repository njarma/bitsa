using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.ViewModels.ClassesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        public Task<IEnumerable<U>> GetAll<U>() where U : BaseViewModel;

        //public Task<IEnumerable<U>> GetFiltered(Expression<Func<U, bool>> condition);
        //public Task<U> GetSingle(Expression<Func<U, bool>> condition);

        //public Task<bool> Exists(Expression<Func<U, bool>> condition);

        public Task<U> Insert<U>(U entity) where U : BaseViewModel;

        public Task Update<U>(U entity) where U : BaseViewModel;

        public Task Delete<U>(U entity) where U : BaseViewModel;

        public Task<IEnumerable<U>> SqlQuery<U>(string sqlString) where U : BaseViewModel;

        public Task<int> SaveChanges();


    }
}
