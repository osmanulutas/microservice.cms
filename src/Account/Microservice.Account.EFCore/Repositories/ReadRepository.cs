using Ardalis.Specification.EntityFrameworkCore;
using Microservice.Account.EFCore.Context;
using Microservice.Account.SharedKernel.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Account.EFCore.Repositories
{
    public class ReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : class, IEntityBase
    {
        protected readonly MicroserviceAccountReadContext _context;

        public ReadRepository(MicroserviceAccountReadContext context) : base(context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }

}
