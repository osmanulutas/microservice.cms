namespace Microservice.Content.SharedKernel.SeedWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
