using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microservice.Content.EFCore.Context;
using Microservice.Content.SharedKernel.SeedWork;

namespace Microservice.Content.EFCore.Repositories
{
    public class EfBaseRepository<T>(MicroserviceContentReadContext context) : RepositoryBase<T>(context), IReadRepositoryBase<T> where T : class, IEntityBase
    {
        private readonly MicroserviceContentReadContext _context = context ?? throw new ArgumentNullException(nameof(context));
        public IUnitOfWork UnitOfWork => null;
    }
}
