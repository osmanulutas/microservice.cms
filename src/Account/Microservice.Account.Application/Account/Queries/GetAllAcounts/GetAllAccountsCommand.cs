using MediatR;
using Microservice.Account.SharedKernel.Models;

namespace Microservice.Account.Application.Account.Queries.GetAllUsers
{
    public class GetAllAccountsCommand : IRequest<ApiResponse<List<GetAllAccountsCommandDto>>>
    {
    }
}
