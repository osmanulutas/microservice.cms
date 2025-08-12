using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microservice.Account.EFCore.Context;
using Microservice.Account.SharedKernel.SeedWork;

namespace Microservice.Account.EFCore.Repositories
{
    public class EfBaseRepository<T>(MicroserviceAccountReadContext context) : RepositoryBase<T>(context), IReadRepositoryBase<T> where T : class, IEntityBase
    {
        private readonly MicroserviceAccountReadContext _context = context ?? throw new ArgumentNullException(nameof(context));
        public IUnitOfWork UnitOfWork => null;
    }
}
