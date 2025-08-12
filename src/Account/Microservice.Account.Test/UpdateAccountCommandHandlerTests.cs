using Microservice.Account.Application.Account.Command.UpdateAccount;
using Microservice.Account.SharedKernel.SeedWork;
using Moq;
using System.Net;
using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;

namespace Microservice.Account.Test
{
    public class UpdateAccountCommandHandlerTests
    {
        private readonly Mock<IWriteRepository<AccountModel>> _mockRepository;
        private readonly UpdateAccountCommandHandler _handler;

        public UpdateAccountCommandHandlerTests()
        {
            _mockRepository = new Mock<IWriteRepository<AccountModel>>();
            _handler = new UpdateAccountCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_WithValidCommand_ShouldReturnSuccessResponse()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var existingAccount = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");
            
            var command = new UpdateAccountCommand
            {
                Id = accountId,
                Name = "Jane",
                SurName = "Smith",
                Email = "jane.smith@example.com",
                BirthDate = new DateOnly(1995, 5, 15),
                PhoneNumber = "9876543210",
                DialCode = "+44"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateAccountSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingAccount);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<AccountModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.Status);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task Handle_WhenAccountNotFound_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var command = new UpdateAccountCommand
            {
                Id = accountId,
                Name = "Jane",
                SurName = "Smith",
                Email = "jane.smith@example.com",
                BirthDate = new DateOnly(1995, 5, 15),
                PhoneNumber = "9876543210",
                DialCode = "+44"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateAccountSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AccountModel)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Account not found", result.Detail);
        }

        [Fact]
        public async Task Handle_WhenArgumentExceptionOccurs_ShouldReturnFailureResponse()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var existingAccount = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");
            
            var command = new UpdateAccountCommand
            {
                Id = accountId,
                Name = "Jane",
                SurName = "Smith",
                Email = "invalid-email", // Invalid email to trigger ArgumentException
                BirthDate = new DateOnly(1995, 5, 15),
                PhoneNumber = "9876543210",
                DialCode = "+44"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateAccountSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingAccount);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Error", result.Title);
        }

        [Fact]
        public async Task Handle_WhenGenericExceptionOccurs_ShouldReturnFailureResponse()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var existingAccount = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");
            
            var command = new UpdateAccountCommand
            {
                Id = accountId,
                Name = "Jane",
                SurName = "Smith",
                Email = "jane.smith@example.com",
                BirthDate = new DateOnly(1995, 5, 15),
                PhoneNumber = "9876543210",
                DialCode = "+44"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateAccountSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingAccount);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<AccountModel>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database connection failed"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Error", result.Title);
            Assert.Contains("An error occurred while updating the account", result.Detail);
        }

        [Fact]
        public async Task Handle_ShouldCallRepositoryMethodsInCorrectOrder()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var existingAccount = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");
            
            var command = new UpdateAccountCommand
            {
                Id = accountId,
                Name = "Jane",
                SurName = "Smith",
                Email = "jane.smith@example.com",
                BirthDate = new DateOnly(1995, 5, 15),
                PhoneNumber = "9876543210",
                DialCode = "+44"
            };

            var callOrder = new List<string>();
            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateAccountSpecification>(), It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("FirstOrDefaultAsync"))
                .ReturnsAsync(existingAccount);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<AccountModel>(), It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("UpdateAsync"))
                .ReturnsAsync(1);
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("SaveChangesAsync"))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(3, callOrder.Count);
            Assert.Equal("FirstOrDefaultAsync", callOrder[0]);
            Assert.Equal("UpdateAsync", callOrder[1]);
            Assert.Equal("SaveChangesAsync", callOrder[2]);
        }

        [Fact]
        public async Task Handle_ShouldUpdateAccountWithCorrectParameters()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var existingAccount = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");
            
            var command = new UpdateAccountCommand
            {
                Id = accountId,
                Name = "Jane",
                SurName = "Smith",
                Email = "jane.smith@example.com",
                BirthDate = new DateOnly(1995, 5, 15),
                PhoneNumber = "9876543210",
                DialCode = "+44"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateAccountSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingAccount);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<AccountModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(command.Name, existingAccount.Name);
            Assert.Equal(command.SurName, existingAccount.SurName);
            Assert.Equal(command.Email, existingAccount.Email);
            Assert.Equal(command.BirthDate, existingAccount.BirthDate);
            Assert.Equal(command.PhoneNumber, existingAccount.PhoneNumber);
            Assert.Equal(command.DialCode, existingAccount.DialCode);
            Assert.NotNull(existingAccount.UpdatedOn);
        }

        [Fact]
        public async Task Handle_WithNullValues_ShouldUpdateOnlySpecifiedFields()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var existingAccount = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");
            var originalName = existingAccount.Name;
            var originalSurName = existingAccount.SurName;
            
            var command = new UpdateAccountCommand
            {
                Id = accountId,
                Name = null,
                SurName = null,
                Email = "jane.smith@example.com",
                BirthDate = new DateOnly(1995, 5, 15),
                PhoneNumber = "9876543210",
                DialCode = "+44"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateAccountSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingAccount);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<AccountModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(originalName, existingAccount.Name); // Should remain unchanged
            Assert.Equal(originalSurName, existingAccount.SurName); // Should remain unchanged
            Assert.Equal("jane.smith@example.com", existingAccount.Email); // Should be updated
            Assert.Equal(new DateOnly(1995, 5, 15), existingAccount.BirthDate); // Should be updated
            Assert.Equal("9876543210", existingAccount.PhoneNumber); // Should be updated
            Assert.Equal("+44", existingAccount.DialCode); // Should be updated
        }

        [Fact]
        public async Task Handle_WithEmptyStrings_ShouldUpdateFieldsWithEmptyStrings()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var existingAccount = new AccountModel("John", "Doe", "john@example.com", new DateOnly(1990, 1, 1), "1234567890", "+1");
            
            var command = new UpdateAccountCommand
            {
                Id = accountId,
                Name = "",
                SurName = "",
                Email = "",
                BirthDate = new DateOnly(1995, 5, 15),
                PhoneNumber = "",
                DialCode = ""
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateAccountSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingAccount);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<AccountModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("", existingAccount.Name);
            Assert.Equal("", existingAccount.SurName);
            Assert.Equal("", existingAccount.Email);
            Assert.Equal("", existingAccount.PhoneNumber);
            Assert.Equal("", existingAccount.DialCode);
        }
    }
}
