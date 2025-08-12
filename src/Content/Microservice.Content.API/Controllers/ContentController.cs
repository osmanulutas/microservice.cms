using MediatR;
using Microservice.Content.Application.Content.Commands.AddContent;
using Microservice.Content.Application.Content.Commands.DeleteContent;
using Microservice.Content.Application.Content.Commands.UpdateContent;
using Microservice.Content.Application.Content.Queries.GetAllContents;
using Microservice.Content.Application.Content.Queries.GetContentById;
using Microservice.Content.SharedKernel.Model;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Content.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ApiResponse<bool>> AddContent([FromBody] AddContentCommand request, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpPut]
        public async Task<ApiResponse<bool>> UpdateContent([FromBody] UpdateContentCommand request, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpGet]
        public async Task<ApiResponse<List<GetAllContentsCommandDto>>> GetAllContents(CancellationToken cancellationToken = default)
        {
            return await mediator.Send(new GetAllContentsCommand(), cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<GetContentByIdCommandDto>> GetContentById(Guid id, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(new GetContentByIdCommand { Id = id }, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse<bool>> DeleteContent(Guid id, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(new DeleteContentCommand { Id = id }, cancellationToken);
        }
    }
}
