using Ardalis.Specification;
using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;

namespace Microservice.Account.Application.Account.Queries.GetAllUsers
{
    public class GetAllAccountsSpecification : Specification<AccountModel, GetAllAccountsCommandDto>
    {
        public GetAllAccountsSpecification() 
        {
            Query.Select(x => new GetAllAccountsCommandDto
            {
                Id = x.Id,
                Name = x.Name,
                SurName = x.SurName,
                BirthDate = x.BirthDate,
                DialCode = x.DialCode,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
            });
            Query.AsNoTracking();
        }
    }
}
