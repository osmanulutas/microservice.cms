using MediatR;
using Microservice.Account.SharedKernel.Models;
using Microservice.Account.SharedKernel.SeedWork;
using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;

namespace Microservice.Account.Application.Account.Queries.GetAllUsers
{
    public class GetAllAccountsCommandHandler(IReadRepository<AccountModel> accountRepository) : IRequestHandler<GetAllAccountsCommand, ApiResponse<List<GetAllAccountsCommandDto>>>
    {
        public async Task<ApiResponse<List<GetAllAccountsCommandDto>>> Handle(GetAllAccountsCommand request, CancellationToken cancellationToken)
        {
            var accountSpec = new GetAllAccountsSpecification();
            var accounts = await accountRepository.ListAsync(accountSpec, cancellationToken);
            return new ApiResponse<List<GetAllAccountsCommandDto>>().ResponseOK(accounts);
        }
    }
}
