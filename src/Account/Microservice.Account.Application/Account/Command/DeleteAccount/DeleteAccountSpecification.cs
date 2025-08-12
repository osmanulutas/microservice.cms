using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;
using Ardalis.Specification;

namespace Microservice.Account.Application.Account.Command.DeleteAccount
{
    public class DeleteAccountSpecification : Specification<AccountModel>
    {
        public DeleteAccountSpecification(Guid id) 
        {
            Query.Where(x => x.Id == id);
        }
    }
}
