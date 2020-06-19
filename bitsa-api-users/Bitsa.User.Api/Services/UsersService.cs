using API.Exceptions;
using AutoMapper;
using Bitsa.User.Api.Exceptions.Bitsa;
using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.Repositories.IRepositories;
using Bitsa.User.Api.Services.IServices;
using Bitsa.User.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bitsa.User.Api.Services
{
    public class UsersService : BaseModel, IUsersService
    {
        private IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersService(DomainContext context, IUsersRepository usersRepository, IMapper mapper) : base(context)
        {
            _userRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper;
        }
        public Task<UsersViewModel> GetById(int userId)
        {
            return _userRepository.GetById(userId);
        }
        public Task<UsersViewModel> GetByAlias(string alias)
        {
            return _userRepository.GetByAlias(alias);
        }
        public async Task AddBalance(int targetId, float balance)
        {
            var entity = _mapper.Map<users>(await GetById(targetId));
            await _userRepository.AddBalance(entity, balance);
            return;
        }
        public async Task SubstractBalance(int sourceId, float balance)
        {
            var entity = _mapper.Map<users>(await GetById(sourceId));
            if (entity.Balance - balance < 0)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "El usuario no dispone del balance ingresado");

            await _userRepository.SubstractBalance(entity, balance);
            return;
        }
        public Task<IEnumerable<UsersViewModel>> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Task<UsersGetViewModel> Save(UsersPostViewModel entity)
        {
            return _userRepository.Save(entity);
        }

        public Task<UsersGetViewModel> Update(UsersPutViewModel entity)
        {
            return _userRepository.Update(entity);
        }

        public Task<UsersGetViewModel> Delete(int entityId)
        {
            return _userRepository.Delete(entityId);
        }

        public async Task TransferBalance(int sourceId, TransferBalanceFilterViewModel filter)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var source = _mapper.Map<users>(await GetById(sourceId));
                    if (source.Balance - filter.Balance < 0)
                        throw new BitsaSourceBalanceInsufficientException();
                    await SubstractBalance(sourceId, filter.Balance);

                    var target = _mapper.Map<users>(await GetByAlias(filter.Alias));
                    if (target == null)
                        throw new BitsaTargetEntityNotExistsException();
                    await AddBalance(target.Id, filter.Balance);

                    transaction.Commit();
                    return;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
