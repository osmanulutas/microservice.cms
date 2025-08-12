using MediatR;
using Microservice.Content.SharedKernel.Model;
using Microservice.Content.SharedKernel.SeedWork;
using System.Net;
using ContentModel = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.Application.Content.Commands.AddContent
{
    public class AddContentCommandHandler(IWriteRepository<ContentModel> contentRepository) : IRequestHandler<AddContentCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(AddContentCommand request, CancellationToken cancellationToken)
        {
            var newContent = new ContentModel(request.Title, request.Description, request.Body, request.AuthorId, request.Category, request.Tags);
            await contentRepository.AddAsync(newContent, cancellationToken);
            var saveResult = await contentRepository.UnitOfWork.SaveChangeAsync(cancellationToken);
            if (saveResult > 0)
                return new ApiResponse<bool>().ResponseOK(true);

            return new ApiResponse<bool>() { Detail = "Content Cant Save", Title = "Data Save Error", Status = (int)HttpStatusCode.BadRequest };
        }
    }
}
