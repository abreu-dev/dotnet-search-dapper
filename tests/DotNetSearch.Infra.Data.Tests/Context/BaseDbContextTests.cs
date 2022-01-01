using DotNetSearch.Domain.Entities;
using DotNetSearch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DotNetSearch.Infra.Data.Tests.Context
{
    public class BaseDbContextTests
    {
        private readonly MyDbContextConcreteClass _myDbContextConcreteClass;

        public BaseDbContextTests()
        {
            _myDbContextConcreteClass = new MyDbContextConcreteClass(
                new DbContextOptionsBuilder<MyDbContextConcreteClass>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        }

        [Fact]
        public void Commit_ShouldCallSaveChangesAsync()
        {
            var resultado = _myDbContextConcreteClass.Commit().GetAwaiter().GetResult();

            Assert.True(resultado);
        }
    }

    public class MyEntityConcreteClass : Entity { }

    public class MyDbContextConcreteClass : BaseDbContext
    {
        public MyDbContextConcreteClass(DbContextOptions options) : base(options) { }

        public DbSet<MyEntityConcreteClass> MyEntityConcreteClass { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(1);
        }
    }
}
