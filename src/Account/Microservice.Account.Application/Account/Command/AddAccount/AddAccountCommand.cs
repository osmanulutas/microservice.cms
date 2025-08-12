using MediatR;
using Microservice.Account.SharedKernel.Models;

namespace Microservice.Account.Application.Account.Command.AddAccount
{
    public class AddAccountCommand : IRequest<ApiResponse<bool>>
    {
        public string Name { get;  set; }
        public string SurName { get;  set; }
        public string Email { get;  set; }
        public DateOnly BirthDate { get;  set; }
        public string PhoneNumber { get;  set; }
        public string DialCode { get;  set; }
    }
}
