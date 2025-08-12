namespace Microservice.Content.Application.Content.Queries.GetAllContents
{
    public class GetAllContentsCommandDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string? Category { get; set; }
        public string? Tags { get; set; }
        public bool IsPublished { get; set; }
    }
}
