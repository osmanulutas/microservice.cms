using MediatR;
using Microservice.Content.SharedKernel.Model;

namespace Microservice.Content.Application.Content.Commands.DeleteContent
{
    public class DeleteContentCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
