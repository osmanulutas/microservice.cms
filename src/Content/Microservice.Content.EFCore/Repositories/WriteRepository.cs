using Ardalis.Specification.EntityFrameworkCore;
using Microservice.Content.EFCore.Context;
using Microservice.Content.SharedKernel.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Content.EFCore.Repositories
{
    public class WriteRepository<T>(MicroserviceContentWriteContext context) : RepositoryBase<T>(context), IWriteRepository<T> where T : class
    {
        protected readonly MicroserviceContentWriteContext Context = context ?? throw new ArgumentNullException(nameof(context));
        public IUnitOfWork UnitOfWork => Context;

        public override async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            Context.Set<T>().Add(entity);
            return await Task.FromResult<T>(entity);
        }
        public override Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(0);
        }
        public override async Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            Context.Set<T>().Remove(entity);
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public void DisableChangeTracking()
        {
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            Context.Set<T>().AsNoTracking();
        }

        public void EnableChangeTracking()
        {
            Context.Set<T>().AsNoTracking();
            Context.ChangeTracker.AutoDetectChangesEnabled |= true;
        }
    }
}
