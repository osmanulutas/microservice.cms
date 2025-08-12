using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;
using Ardalis.Specification;

namespace Microservice.Account.Application.Account.Queries.GetUserById
{
    public class GetAccountByIdSpecification : Specification<AccountModel, GetAccountByIdCommandDto>
    {
        public GetAccountByIdSpecification(Guid id) 
        {
            Query.Where(x=>x.Id == id).Select(x => new GetAccountByIdCommandDto
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
