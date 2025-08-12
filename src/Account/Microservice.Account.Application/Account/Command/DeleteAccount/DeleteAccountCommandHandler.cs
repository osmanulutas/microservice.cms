using MediatR;
using Microservice.Account.SharedKernel.Models;
using Microservice.Account.SharedKernel.SeedWork;
using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;

namespace Microservice.Account.Application.Account.Command.DeleteAccount
{
    public class DeleteAccountCommandHandler(IWriteRepository<AccountModel> accountRepository) : IRequestHandler<DeleteAccountCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var accountSpec = new DeleteAccountSpecification(request.Id);
            var account = await accountRepository.FirstOrDefaultAsync(accountSpec, cancellationToken);
            if (account == null)
                return new ApiResponse<bool>().NotFound("User didnt Find");

            account.DeleteAccount();
            await accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResponse<bool>().ResponseOK(true);


        }
    }
}
