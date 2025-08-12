using MediatR;
using Microservice.Account.SharedKernel.Models;
using Microservice.Account.SharedKernel.SeedWork;
using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;

namespace Microservice.Account.Application.Account.Command.UpdateAccount
{
    public class UpdateAccountCommandHandler(IWriteRepository<AccountModel> accountRepository) : IRequestHandler<UpdateAccountCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var accountSpec = new UpdateAccountSpecification(request.Id);
            var account = await accountRepository.FirstOrDefaultAsync(accountSpec, cancellationToken);

            if (account == null)
                return new ApiResponse<bool>().NotFound("Account not found");
            
            try
            {
                account.UpdateAccount(
                    name: request.Name,
                    surName: request.SurName,
                    email: request.Email,
                    birthDate: request.BirthDate,
                    phoneNumber: request.PhoneNumber,
                    dialCode: request.DialCode
                );

                await accountRepository.UpdateAsync(account, cancellationToken);
                await accountRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                return new ApiResponse<bool>().ResponseOK(true);
            }
            catch (ArgumentException ex)
            {
                return new ApiResponse<bool>().Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>().Failure("An error occurred while updating the account, Detail: " + ex.Message);
            }
        }
    }
}
