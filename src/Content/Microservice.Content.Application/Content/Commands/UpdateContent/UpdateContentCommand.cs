using MediatR;
using Microservice.Content.SharedKernel.Model;

namespace Microservice.Content.Application.Content.Commands.UpdateContent
{
    public class UpdateContentCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Body { get; set; }
        public Guid? AuthorId { get; set; }
        public string? Category { get; set; }
        public string? Tags { get; set; }
    }
}
