namespace Microservice.Account.Application.Account.Queries.GetUserById
{
    public class GetAccountByIdCommandDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? Email { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DialCode { get; set; }
    }
}
