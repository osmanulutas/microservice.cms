using MediatR;
using Microservice.Content.SharedKernel.Model;
using Microservice.Content.SharedKernel.SeedWork;
using System.Net;
using ContentModel = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.Application.Content.Commands.UpdateContent
{
    public class UpdateContentCommandHandler(IWriteRepository<ContentModel> contentRepository) : IRequestHandler<UpdateContentCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
        {
            var contentSpec = new UpdateContentSpecification(request.Id);
            var content = await contentRepository.FirstOrDefaultAsync(contentSpec, cancellationToken);
            if (content == null)
                return new ApiResponse<bool>() { Detail = "Content not found", Title = "Not Found", Status = (int)HttpStatusCode.NotFound };

            content.UpdateContent(request.AuthorId.GetValueOrDefault(), request.Title, request.Description, request.Body,  request.Category, request.Tags);
            await contentRepository.UpdateAsync(content, cancellationToken);
            var saveResult = await contentRepository.SaveChangesAsync(cancellationToken);
            if (saveResult > 0)
                return new ApiResponse<bool>().ResponseOK(true);

            return new ApiResponse<bool>() { Detail = "Content Cant Update", Title = "Data Update Error", Status = (int)HttpStatusCode.BadRequest };
        }
    }
}
