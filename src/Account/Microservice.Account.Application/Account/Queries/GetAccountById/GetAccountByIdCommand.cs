using MediatR;
using Microservice.Account.SharedKernel.Models;

namespace Microservice.Account.Application.Account.Queries.GetUserById
{
    public class GetAccountByIdCommand : IRequest<ApiResponse<GetAccountByIdCommandDto>>
    {
        public Guid Id { get; set; }
    }
}
