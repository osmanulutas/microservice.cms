using Ardalis.Specification;
using MediatR;
using Microservice.Account.Application.Account.Queries.GetUserById;
using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;
namespace Microservice.Account.Application.Account.Command.UpdateAccount
{
    public class UpdateAccountSpecification : Specification<AccountModel>
    {
        public UpdateAccountSpecification(Guid id) 
        {
            Query.Where(x => x.Id == id);
        } 
    }
}
