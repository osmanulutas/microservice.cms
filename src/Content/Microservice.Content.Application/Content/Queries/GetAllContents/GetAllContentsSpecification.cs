using Ardalis.Specification;
using ContentModel = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.Application.Content.Queries.GetAllContents
{
    public class GetAllContentsSpecification : Specification<ContentModel, GetAllContentsCommandDto>
    {
        public GetAllContentsSpecification()
        {
            Query.Select(x => new GetAllContentsCommandDto
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Description = x.Description,
                     AuthorId = x.AuthorId,
                     Category = x.Category,
                     Tags = x.Tags,
                     IsPublished = x.IsPublished
                 });
            Query.AsNoTracking();
        }
    }
}
