using MediatR;
using Microservice.Account.SharedKernel.Models;

namespace Microservice.Account.Application.Account.Command.DeleteAccount
{
    public class DeleteAccountCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
