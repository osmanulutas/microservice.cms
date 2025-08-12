using Ardalis.Specification;

namespace Microservice.Account.SharedKernel.SeedWork
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IEntityBase
    {

    }

    public interface IWriteRepository<T> : IRepositoryBase<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
        void DisableChangeTracking();
        void EnableChangeTracking();
    }

}
