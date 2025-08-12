using MediatR;
using Microservice.Account.SharedKernel.Models;
using Microservice.Account.SharedKernel.SeedWork;
using System.Net;
using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;
namespace Microservice.Account.Application.Account.Command.AddAccount
{
    public class AddAccountCommandHandler(IWriteRepository<AccountModel> accountRepository) : IRequestHandler<AddAccountCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var newAccount = new AccountModel(request.Name, request.SurName, request.Email, request.BirthDate, request.PhoneNumber, request.DialCode);
            await accountRepository.AddAsync(newAccount, cancellationToken);
            var saveResult = await accountRepository.SaveChangesAsync(cancellationToken);
            if (saveResult > 0)
                return new ApiResponse<bool>().ResponseOK(true);


            return new ApiResponse<bool>() { Detail = "Account Cant Save", Title="Data Save Error", Status = (int)HttpStatusCode.BadRequest };


        }
    }
}
