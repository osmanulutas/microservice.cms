using Microservice.Account.SharedKernel.SeedWork;

namespace Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity
{
    public class Account : Entity
    {
        public string? Name { get; private set; }
        public string? SurName { get; private set; }
        public string? Email { get; private set; }
        public DateOnly? BirthDate { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? DialCode { get; private set; }

        public Account() { }

        public Account(string? name, string? surName, string? eMail, DateOnly birthDate, string? phoneNumber, string? dialCode)
        {
            Id = Guid.NewGuid();
            Name = name;
            SurName = surName;
            Email = eMail;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            DialCode = dialCode;
            CreatedOn = DateTime.UtcNow;
        }
        public void DeleteAccount()
        {
            DeletedOn = DateTime.UtcNow;
        }
        public void Updated()
        {
            UpdatedOn = DateTime.UtcNow;
        }
        public void UpdateAccount(
        string? name = null,
        string? surName = null,
        string? email = null,
        DateOnly? birthDate = null,
        string? phoneNumber = null,
        string? dialCode = null)
        {
            if (name is not null) Name = name;
            if (surName is not null) SurName = surName;
            if (email is not null)
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("Email cannot be empty", nameof(email));
                Email = email;
            }
            if (birthDate.HasValue) BirthDate = birthDate;
            if (phoneNumber is not null) PhoneNumber = phoneNumber;
            if (dialCode is not null) DialCode = dialCode;

            Updated();
        }


    }
}
