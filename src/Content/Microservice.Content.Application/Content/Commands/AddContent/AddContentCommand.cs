using MediatR;
using Microservice.Content.SharedKernel.Model;

namespace Microservice.Content.Application.Content.Commands.AddContent
{
    public class AddContentCommand : IRequest<ApiResponse<bool>>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Body { get; set; }
        public Guid AuthorId{ get; set; }
        public string? Category { get; set; }
        public string? Tags { get; set; }
    }
}
