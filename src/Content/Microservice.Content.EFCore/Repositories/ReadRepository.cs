using Ardalis.Specification.EntityFrameworkCore;
using Microservice.Content.EFCore.Context;
using Microservice.Content.SharedKernel.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Microservice.Content.EFCore.Repositories
{
    public class ReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : class, IEntityBase
    {
        protected readonly MicroserviceContentReadContext _context;

        public ReadRepository(MicroserviceContentReadContext context) : base(context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
    }
}
