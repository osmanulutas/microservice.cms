namespace Microservice.Content.Application.Content.Queries.GetContentById
{
    public class GetContentByIdCommandDto
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Body { get; set; }
        public string? Category { get; set; }
        public string? Tags { get; set; }
        public bool IsPublished { get; set; }
    }
}
