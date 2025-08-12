using FluentValidation.TestHelper;
using Microservice.Account.Application.Account.Command.AddAccount;
using Xunit;

namespace Microservice.Account.Test
{
    public class AddAccountCommandValidatorTests
    {
        private readonly AddAccountCommandValidator _validator;

        public AddAccountCommandValidatorTests()
        {
            _validator = new AddAccountCommandValidator();
        }

        [Fact]
        public void ValidCommand_ShouldPassValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "John",
                SurName = "Doe",
                Email = "john.doe@example.com",
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = "1234567890",
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Name_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = null,
                SurName = "Doe",
                Email = "john.doe@example.com",
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = "1234567890",
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Name Cant be null");
        }

        [Fact]
        public void Name_WhenTooShort_ShouldFailValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "Jo",
                SurName = "Doe",
                Email = "john.doe@example.com",
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = "1234567890",
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Name Must be longer then 3 character");
        }

        [Fact]
        public void SurName_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "John",
                SurName = null,
                Email = "john.doe@example.com",
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = "1234567890",
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SurName);
            result.ShouldHaveValidationErrorFor(x => x.SurName)
                .WithErrorMessage("SurName Cant be null");
        }

        [Fact]
        public void SurName_WhenTooShort_ShouldFailValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "John",
                SurName = "Do",
                Email = "john.doe@example.com",
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = "1234567890",
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SurName);
            result.ShouldHaveValidationErrorFor(x => x.SurName)
                .WithErrorMessage("SurName Must be longer then 3 character");
        }

        [Fact]
        public void Email_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "John",
                SurName = "Doe",
                Email = null,
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = "1234567890",
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email Cant be null");
        }

        [Fact]
        public void Email_WhenInvalidFormat_ShouldFailValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "John",
                SurName = "Doe",
                Email = "invalid-email",
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = "1234567890",
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Mail adress must be real");
        }

        [Fact]
        public void DialCode_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "John",
                SurName = "Doe",
                Email = "john.doe@example.com",
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = "1234567890",
                DialCode = null
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DialCode);
            result.ShouldHaveValidationErrorFor(x => x.DialCode)
                .WithErrorMessage("Dial Code Cant be null");
        }

        [Fact]
        public void PhoneNumber_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "John",
                SurName = "Doe",
                Email = "john.doe@example.com",
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = null,
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("Phone Number Cant be null");
        }

        [Fact]
        public void PhoneNumber_WhenTooShort_ShouldFailValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "John",
                SurName = "Doe",
                Email = "john.doe@example.com",
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = "12345",
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("Phone Number must be longer then 6 digit");
        }

        [Fact]
        public void BirthDate_WhenMinValue_ShouldFailValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "John",
                SurName = "Doe",
                Email = "john.doe@example.com",
                BirthDate = DateOnly.MinValue,
                PhoneNumber = "1234567890",
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BirthDate);
            result.ShouldHaveValidationErrorFor(x => x.BirthDate)
                .WithErrorMessage("Birth Date is required");
        }

        [Fact]
        public void BirthDate_WhenInFuture_ShouldFailValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "John",
                SurName = "Doe",
                Email = "john.doe@example.com",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(1)),
                PhoneNumber = "1234567890",
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BirthDate);
            result.ShouldHaveValidationErrorFor(x => x.BirthDate)
                .WithErrorMessage("Birth Date cannot be in the future");
        }

        [Fact]
        public void BirthDate_WhenToday_ShouldPassValidation()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "John",
                SurName = "Doe",
                Email = "john.doe@example.com",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow),
                PhoneNumber = "1234567890",
                DialCode = "+1"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.BirthDate);
        }

        [Fact]
        public void MultipleValidationErrors_ShouldAllBeReported()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = null,
                SurName = null,
                Email = "invalid-email",
                BirthDate = DateOnly.MinValue,
                PhoneNumber = null,
                DialCode = null
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.SurName);
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.BirthDate);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
            result.ShouldHaveValidationErrorFor(x => x.DialCode);
        }
    }
}
