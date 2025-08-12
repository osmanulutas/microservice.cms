using Ardalis.Specification;
using ContentModel = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.Application.Content.Commands.UpdateContent
{
    public class UpdateContentSpecification : Specification<ContentModel>
    {
        public UpdateContentSpecification(Guid id)
        {
            Query.Where(x => x.Id == id);
        }
    }
}
