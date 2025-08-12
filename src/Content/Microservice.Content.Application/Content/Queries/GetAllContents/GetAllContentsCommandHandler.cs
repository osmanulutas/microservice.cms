using MediatR;
using Microservice.Content.SharedKernel.Model;
using Microservice.Content.SharedKernel.SeedWork;
using ContentModel = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.Application.Content.Queries.GetAllContents
{
    public class GetAllContentsCommandHandler(IReadRepository<ContentModel> contentRepository) : IRequestHandler<GetAllContentsCommand, ApiResponse<List<GetAllContentsCommandDto>>>
    {
        public async Task<ApiResponse<List<GetAllContentsCommandDto>>> Handle(GetAllContentsCommand request, CancellationToken cancellationToken)
        {
            var contentSpec = new GetAllContentsSpecification();
            var contents = await contentRepository.ListAsync(contentSpec, cancellationToken);
            return new ApiResponse<List<GetAllContentsCommandDto>>().ResponseOK(contents);
        }
    }
}
