using MediatR;
using Microservice.Account.Application.Account.Command.AddAccount;
using Microservice.Account.Application.Account.Command.DeleteAccount;
using Microservice.Account.Application.Account.Command.UpdateAccount;
using Microservice.Account.Application.Account.Queries.GetAllUsers;
using Microservice.Account.Application.Account.Queries.GetUserById;
using Microservice.Account.SharedKernel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ApiResponse<bool>> AddAccount([FromBody] AddAccountCommand request, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(request, cancellationToken);
        }
        [HttpPut]
        public async Task<ApiResponse<bool>> UpdateAccount([FromBody] UpdateAccountCommand request, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(request, cancellationToken);
        }
        [HttpGet]
        public async Task<ApiResponse<List<GetAllAccountsCommandDto>>> GetAllAccount(CancellationToken cancellationToken= default)
        {
            return await mediator.Send(new GetAllAccountsCommand(), cancellationToken);
        }
        [HttpGet("{id}")]
        public async Task<ApiResponse<GetAccountByIdCommandDto>> GetAccountById(Guid id, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(new GetAccountByIdCommand { Id = id }, cancellationToken);
        }
        [HttpDelete("{id}")]
        public async Task<ApiResponse<bool>> DeleteAccount(Guid id,CancellationToken cancellationToken = default)
        {
            return await mediator.Send(new DeleteAccountCommand { Id = id }, cancellationToken);
        }





    }

}
