using Ardalis.Specification;
using ContentModel = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.Application.Content.Queries.GetContentById
{
    public class GetContentByIdSpecification : Specification<ContentModel, GetContentByIdCommandDto>
    {
        public GetContentByIdSpecification(Guid id)
        {
            Query.Where(x => x.Id == id);
            Query.Select(x => new GetContentByIdCommandDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Body = x.Body,
                AuthorId = x.AuthorId,
                Category = x.Category,
                Tags = x.Tags,
                IsPublished = x.IsPublished,
            });
            Query.AsNoTracking();
        }
    }
}
