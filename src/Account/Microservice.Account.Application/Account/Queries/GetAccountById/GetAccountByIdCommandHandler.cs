using MediatR;
using Microservice.Account.SharedKernel.Models;
using Microservice.Account.SharedKernel.SeedWork;
using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;

namespace Microservice.Account.Application.Account.Queries.GetUserById
{
    public class GetAccountByIdCommandHandler(IReadRepository<AccountModel> accountRepository) : IRequestHandler<GetAccountByIdCommand, ApiResponse<GetAccountByIdCommandDto>>
    {
        public async Task<ApiResponse<GetAccountByIdCommandDto>> Handle(GetAccountByIdCommand request, CancellationToken cancellationToken)
        {
            var userSpec = new GetAccountByIdSpecification(request.Id);
            var user = await accountRepository.FirstOrDefaultAsync(userSpec, cancellationToken);
            return new ApiResponse<GetAccountByIdCommandDto>().ResponseOK(user);
        }
    }
}
