using Ardalis.Specification;
using ContentModel = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.Application.Content.Commands.DeleteContent
{
    public class DeleteContentSpecification : Specification<ContentModel>
    {
        public DeleteContentSpecification(Guid id)
        {
            Query.Where(x => x.Id == id);
        }
    }
}
