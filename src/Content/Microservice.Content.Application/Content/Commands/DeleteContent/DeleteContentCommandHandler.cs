using MediatR;
using Microservice.Content.SharedKernel.Model;
using Microservice.Content.SharedKernel.SeedWork;
using System.Net;
using ContentModel = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.Application.Content.Commands.DeleteContent
{
    public class DeleteContentCommandHandler(IWriteRepository<ContentModel> contentRepository) : IRequestHandler<DeleteContentCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteContentCommand request, CancellationToken cancellationToken)
        {
            var contentSpec = new DeleteContentSpecification(request.Id);
            var content = await contentRepository.FirstOrDefaultAsync(contentSpec, cancellationToken);
            if (content == null)
                return new ApiResponse<bool>() { Detail = "Content not found", Title = "Not Found", Status = (int)HttpStatusCode.NotFound };

            content.DeleteContent();

            await contentRepository.UpdateAsync(content, cancellationToken);
            var saveResult = await contentRepository.UnitOfWork.SaveChangeAsync(cancellationToken);

            if (saveResult > 0)
                return new ApiResponse<bool>().ResponseOK(true);

            return new ApiResponse<bool>() { Detail = "Content Cant Delete", Title = "Data Delete Error", Status = (int)HttpStatusCode.BadRequest };
        }
    }
}
