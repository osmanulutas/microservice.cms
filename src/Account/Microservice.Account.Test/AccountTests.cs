using Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity;
using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;
using Xunit;

namespace Microservice.Account.Test
{
    public class AccountTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateAccount()
        {
            // Arrange
            var name = "John";
            var surName = "Doe";
            var email = "john.doe@example.com";
            var birthDate = new DateOnly(1990, 1, 1);
            var phoneNumber = "1234567890";
            var dialCode = "+1";

            // Act
            var account = new AccountModel(name, surName, email, birthDate, phoneNumber, dialCode);

            // Assert
            Assert.NotEqual(Guid.Empty, account.Id);
            Assert.Equal(name, account.Name);
            Assert.Equal(surName, account.SurName);
            Assert.Equal(email, account.Email);
            Assert.Equal(birthDate, account.BirthDate);
            Assert.Equal(phoneNumber, account.PhoneNumber);
            Assert.Equal(dialCode, account.DialCode);
            Assert.True(account.CreatedOn <= DateTime.UtcNow);
            Assert.Null(account.UpdatedOn);
            Assert.Null(account.DeletedOn);
        }

        [Fact]
        public void Constructor_WithNullParameters_ShouldCreateAccountWithNullValues()
        {
            // Arrange
            var birthDate = new DateOnly(1990, 1, 1);

            // Act
            var account = new AccountModel(null, null, null, birthDate, null, null);

            // Assert
            Assert.NotEqual(Guid.Empty, account.Id);
            Assert.Null(account.Name);
            Assert.Null(account.SurName);
            Assert.Null(account.Email);
            Assert.Equal(birthDate, account.BirthDate);
            Assert.Null(account.PhoneNumber);
            Assert.Null(account.DialCode);
        }

        [Fact]
        public void DeleteAccount_ShouldSetDeletedOnTimestamp()
        {
            // Arrange
            var account = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");

            // Act
            account.DeleteAccount();

            // Assert
            Assert.NotNull(account.DeletedOn);
            Assert.True(account.DeletedOn <= DateTime.UtcNow);
        }

        [Fact]
        public void Updated_ShouldSetUpdatedOnTimestamp()
        {
            // Arrange
            var account = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");

            // Act
            account.Updated();

            // Assert
            Assert.NotNull(account.UpdatedOn);
            Assert.True(account.UpdatedOn <= DateTime.UtcNow);
        }

        [Fact]
        public void UpdateAccount_WithValidParameters_ShouldUpdateFields()
        {
            // Arrange
            var account = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");
            var newName = "Jane";
            var newEmail = "jane@example.com";

            // Act
            account.UpdateAccount(name: newName, email: newEmail);

            // Assert
            Assert.Equal(newName, account.Name);
            Assert.Equal(newEmail, account.Email);
            Assert.NotNull(account.UpdatedOn);
        }

        [Fact]
        public void UpdateAccount_WithEmptyEmail_ShouldThrowArgumentException()
        {
            // Arrange
            var account = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.UpdateAccount(email: ""));
            Assert.Equal("Email cannot be empty", exception.Message);
        }

        [Fact]
        public void UpdateAccount_WithWhitespaceEmail_ShouldThrowArgumentException()
        {
            // Arrange
            var account = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.UpdateAccount(email: "   "));
            Assert.Equal("Email cannot be empty", exception.Message);
        }

        [Fact]
        public void UpdateAccount_WithNullParameters_ShouldNotUpdateFields()
        {
            // Arrange
            var originalName = "John";
            var originalEmail = "john@example.com";
            var account = new AccountModel(originalName, "Doe", originalEmail, new DateOnly(1990, 1, 1), "1234567890", "+1");

            // Act
            account.UpdateAccount(name: null, email: null);

            // Assert
            Assert.Equal(originalName, account.Name);
            Assert.Equal(originalEmail, account.Email);
        }

        [Fact]
        public void UpdateAccount_ShouldCallUpdatedMethod()
        {
            // Arrange
            var account = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");

            // Act
            account.UpdateAccount(name: "Jane");

            // Assert
            Assert.NotNull(account.UpdatedOn);
        }

        [Fact]
        public void UpdateAccount_WithBirthDate_ShouldUpdateBirthDate()
        {
            // Arrange
            var account = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");
            var newBirthDate = new DateOnly(1995, 5, 15);

            // Act
            account.UpdateAccount(birthDate: newBirthDate);

            // Assert
            Assert.Equal(newBirthDate, account.BirthDate);
        }

        [Fact]
        public void UpdateAccount_WithPhoneNumberAndDialCode_ShouldUpdateBothFields()
        {
            // Arrange
            var account = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");
            var newPhoneNumber = "9876543210";
            var newDialCode = "+44";

            // Act
            account.UpdateAccount(phoneNumber: newPhoneNumber, dialCode: newDialCode);

            // Assert
            Assert.Equal(newPhoneNumber, account.PhoneNumber);
            Assert.Equal(newDialCode, account.DialCode);
        }
    }
}
