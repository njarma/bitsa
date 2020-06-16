using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bitsa.User.Api.Model.Classes;
using System;

namespace Bitsa.User.Api.UnitTest.Repositories
{
    public class BaseRepositoryTest
    {
        protected DomainContext InMemoryDbContext;
        protected IMapper IMapper;

        public BaseRepositoryTest()
        {
            BuildDb();
            IMapper = new Mapper(new MapperConfiguration(config => config.AddMaps(typeof(DomainContext).Assembly)));
        }

        protected void BuildDb()
        {
            var options = new DbContextOptionsBuilder<DomainContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;

            InMemoryDbContext = new DomainContext(options);
            InMemoryDbContext.Database.EnsureDeleted();
            InMemoryDbContext.Database.EnsureCreated();
        }
    }
}
