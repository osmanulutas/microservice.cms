using FluentValidation;

namespace Microservice.Account.Application.Account.Command.AddAccount
{
    public class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
    {
        public AddAccountCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name Cant be null")
                .MinimumLength(3).WithMessage("Name Must be longer then 3 character");

            RuleFor(x => x.SurName)
                .NotNull().WithMessage("SurName Cant be null")
                .MinimumLength(3).WithMessage("SurName Must be longer then 3 character");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email Cant be null")
                .EmailAddress().WithMessage("Mail adress must be real");

            RuleFor(x => x.DialCode)
                .NotNull().WithMessage("Dial Code Cant be null");

            RuleFor(x => x.PhoneNumber)
                .NotNull().WithMessage("Phone Number Cant be null")
                .MinimumLength(6).WithMessage("Phone Number must be longer then 6 digit");

            RuleFor(x => x.BirthDate)
                .Must(d => d > DateOnly.MinValue)
                .WithMessage("Birth Date is required")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("Birth Date cannot be in the future");

        }
    }
}
