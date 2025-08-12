using MediatR;
using Microservice.Content.SharedKernel.Model;
using Microservice.Content.SharedKernel.SeedWork;
using System.Net;
using ContentModel = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.Application.Content.Queries.GetContentById
{
    public class GetContentByIdCommandHandler(IReadRepository<ContentModel> contentRepository) : IRequestHandler<GetContentByIdCommand, ApiResponse<GetContentByIdCommandDto>>
    {
        public async Task<ApiResponse<GetContentByIdCommandDto>> Handle(GetContentByIdCommand request, CancellationToken cancellationToken)
        {
            var contentSpec = new GetContentByIdSpecification(request.Id);
            var content = await contentRepository.FirstOrDefaultAsync(contentSpec, cancellationToken);
            if (content == null)
                return new ApiResponse<GetContentByIdCommandDto>() { Detail = "Content not found", Title = "Not Found", Status = (int)HttpStatusCode.NotFound };

            return new ApiResponse<GetContentByIdCommandDto>().ResponseOK(content);
        }
    }
}
