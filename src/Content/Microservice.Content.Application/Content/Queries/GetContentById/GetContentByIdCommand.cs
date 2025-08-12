using MediatR;
using Microservice.Content.SharedKernel.Model;

namespace Microservice.Content.Application.Content.Queries.GetContentById
{
    public class GetContentByIdCommand : IRequest<ApiResponse<GetContentByIdCommandDto>>
    {
        public Guid Id { get; set; }
    }
}
