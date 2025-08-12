using FluentValidation;

namespace Microservice.Content.Application.Content.Commands.AddContent
{
    public class AddContentCommandValidator : AbstractValidator<AddContentCommand>
    {
        public AddContentCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Body is required")
                .MaximumLength(10000).WithMessage("Body cannot exceed 10000 characters");

            RuleFor(x => x.Category)
                .MaximumLength(50).WithMessage("Category cannot exceed 50 characters");

            RuleFor(x => x.Tags)
                .MaximumLength(200).WithMessage("Tags cannot exceed 200 characters");
        }
    }
}
