using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.Repositories.IRepositories;
using Bitsa.User.Api.ViewModels.ClassesBase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        protected readonly DomainContext _context;
        protected readonly IMapper _mapper;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(DomainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<T>();
        }
        public virtual Task<IEnumerable<U>> GetAll<U>() where U : BaseViewModel
        {
            return Task.FromResult(_mapper.Map<IEnumerable<U>>(_dbSet.AsEnumerable()));
        }

        //public virtual Task<IEnumerable<U>> GetFiltered(Expression<Func<U, bool>> condition)
        //{
        //    return Task.FromResult(_dbSet.UseAsDataSource(_mapper.ConfigurationProvider).For<U>().Where(condition).AsEnumerable());
        //}

        //public virtual Task<U> GetSingle(Expression<Func<U, bool>> condition)
        //{
        //    return Task.FromResult(_dbSet.UseAsDataSource(_mapper.ConfigurationProvider).For<U>().FirstOrDefault(condition));
        //}

        //public virtual Task<bool> Exists(Expression<Func<U, bool>> condition)
        //{
        //    return Task.FromResult(_dbSet.UseAsDataSource(_mapper.ConfigurationProvider).For<U>().Any(condition));
        //}

        public virtual Task<U> Insert<U>(U entity) where U : BaseViewModel
        {
            T dbObj = _mapper.Map<T>(entity);
            return Task.FromResult(_mapper.Map<U>(_dbSet.Add(dbObj).Entity));
        }

        public virtual Task Update<U>(U entity) where U : BaseViewModel
        {
            T dbObj = _mapper.Map<T>(entity);
            _context.Entry(dbObj).State = EntityState.Modified;
            return Task.FromResult(_dbSet.Update(dbObj));
        }

        public virtual Task Delete<U>(U entity) where U : BaseViewModel
        {
            return Task.FromResult(_dbSet.Remove(_mapper.Map<T>(entity)).Entity);
        }

        public virtual Task<IEnumerable<U>> SqlQuery<U>(string sqlString) where U : BaseViewModel
        {
            return Task.FromResult(_mapper.Map<IEnumerable<U>>(_dbSet.FromSql(sqlString)));
        }

        public virtual Task<int> SaveChanges()
        {
            return Task.FromResult(_context.SaveChanges());
        }
    }
}
