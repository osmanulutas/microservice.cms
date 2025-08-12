using Microservice.Content.SharedKernel.SeedWork;

namespace Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity
{
    public class Content : EntityBase<Guid>
    {
        public string? Title { get; private set; }
        public string? Description { get; private set; }
        public string? Body { get; private set; }
        public Guid AuthorId { get; private set; }
        public string? Category { get; private set; }
        public string? Tags { get; private set; }
        public bool IsPublished { get; private set; }
        public DateTime? PublishedDate { get; private set; }

        public Content() { }

        public Content(string? title, string? description, string? body, Guid authorId, string? category, string? tags)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Body = body;
            AuthorId = authorId;
            Category = category;
            Tags = tags;
            IsPublished = false;
            CreatedOn = DateTime.UtcNow;
        }

        public void DeleteContent()
        {
            DeletedOn = DateTime.UtcNow;
        }

        public void Updated()
        {
            UpdatedOn = DateTime.UtcNow;
        }

        public void UpdateContent(
            Guid authorId,
            string? title = null,
            string? description = null,
            string? body = null,
            string? category = null,
            string? tags = null)
        {
            if (title is not null) Title = title;
            if (description is not null) Description = description;
            if (body is not null) Body = body;
            AuthorId = authorId;
            if (category is not null) Category = category;
            if (tags is not null) Tags = tags;

            Updated();
        }

        public void Publish()
        {
            IsPublished = true;
            PublishedDate = DateTime.UtcNow;
            Updated();
        }

        public void Unpublish()
        {
            IsPublished = false;
            PublishedDate = null;
            Updated();
        }
    }
}
