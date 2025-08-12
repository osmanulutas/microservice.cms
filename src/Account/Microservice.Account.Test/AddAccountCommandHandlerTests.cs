using MediatR;
using Microservice.Account.Application.Account.Command.AddAccount;
using AccountEntity = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;
using Microservice.Account.SharedKernel.Models;
using Microservice.Account.SharedKernel.SeedWork;
using Moq;
using System.Net;
using Xunit;

namespace Microservice.Account.Test
{
    public class AddAccountCommandHandlerTests
    {
        private readonly Mock<IWriteRepository<AccountEntity>> _mockRepository;
        private readonly AddAccountCommandHandler _handler;

        public AddAccountCommandHandlerTests()
        {
            _mockRepository = new Mock<IWriteRepository<AccountEntity>>();
            _handler = new AddAccountCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_WithValidCommand_ShouldReturnSuccessResponse()
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

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<AccountEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AccountEntity("Test", "Test", "test@test.com", new DateOnly(1990, 1, 1), "123", "+1"));
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.Status);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task Handle_WhenSaveFails_ShouldReturnErrorResponse()
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

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<AccountEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AccountEntity("Test", "Test", "test@test.com", new DateOnly(1990, 1, 1), "123", "+1"));
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Data Save Error", result.Title);
            Assert.Equal("Account Cant Save", result.Detail);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.Status);
        }

        [Fact]
        public async Task Handle_ShouldCreateAccountWithCorrectParameters()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "Jane",
                SurName = "Smith",
                Email = "jane.smith@example.com",
                BirthDate = new DateOnly(1995, 5, 15),
                PhoneNumber = "9876543210",
                DialCode = "+44"
            };

            AccountEntity capturedAccount = null;
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<AccountEntity>(), It.IsAny<CancellationToken>()))
                .Callback<AccountEntity, CancellationToken>((account, token) => capturedAccount = account)
                .ReturnsAsync(new AccountEntity("Test", "Test", "test@test.com", new DateOnly(1990, 1, 1), "123", "+1"));
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(capturedAccount);
            Assert.Equal(command.Name, capturedAccount.Name);
            Assert.Equal(command.SurName, capturedAccount.SurName);
            Assert.Equal(command.Email, capturedAccount.Email);
            Assert.Equal(command.BirthDate, capturedAccount.BirthDate);
            Assert.Equal(command.PhoneNumber, capturedAccount.PhoneNumber);
            Assert.Equal(command.DialCode, capturedAccount.DialCode);
            Assert.NotEqual(Guid.Empty, capturedAccount.Id);
            Assert.True(capturedAccount.CreatedOn <= DateTime.UtcNow);
        }

        [Fact]
        public async Task Handle_ShouldCallRepositoryMethodsInCorrectOrder()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "Test",
                SurName = "User",
                Email = "test@example.com",
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = "1234567890",
                DialCode = "+1"
            };

            var callOrder = new List<string>();
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<AccountEntity>(), It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("AddAsync"))
                .ReturnsAsync(new AccountEntity("Test", "Test", "test@test.com", new DateOnly(1990, 1, 1), "123", "+1"));
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("SaveChangesAsync"))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(2, callOrder.Count);
            Assert.Equal("AddAsync", callOrder[0]);
            Assert.Equal("SaveChangesAsync", callOrder[1]);
        }

        [Fact]
        public async Task Handle_WithNullValues_ShouldCreateAccountWithNullValues()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = null,
                SurName = null,
                Email = null,
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = null,
                DialCode = null
            };

            AccountEntity capturedAccount = null;
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<AccountEntity>(), It.IsAny<CancellationToken>()))
                .Callback<AccountEntity, CancellationToken>((account, token) => capturedAccount = account)
                .ReturnsAsync(new AccountEntity("Test", "Test", "test@test.com", new DateOnly(1990, 1, 1), "123", "+1"));
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(capturedAccount);
            Assert.Null(capturedAccount.Name);
            Assert.Null(capturedAccount.SurName);
            Assert.Null(capturedAccount.Email);
            Assert.Equal(command.BirthDate, capturedAccount.BirthDate);
            Assert.Null(capturedAccount.PhoneNumber);
            Assert.Null(capturedAccount.DialCode);
        }

        [Fact]
        public async Task Handle_WithEmptyStrings_ShouldCreateAccountWithEmptyStrings()
        {
            // Arrange
            var command = new AddAccountCommand
            {
                Name = "",
                SurName = "",
                Email = "",
                BirthDate = new DateOnly(1990, 1, 1),
                PhoneNumber = "",
                DialCode = ""
            };

            AccountEntity capturedAccount = null;
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<AccountEntity>(), It.IsAny<CancellationToken>()))
                .Callback<AccountEntity, CancellationToken>((account, token) => capturedAccount = account)
                .ReturnsAsync(new AccountEntity("Test", "Test", "test@test.com", new DateOnly(1990, 1, 1), "123", "+1"));
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(capturedAccount);
            Assert.Equal("", capturedAccount.Name);
            Assert.Equal("", capturedAccount.SurName);
            Assert.Equal("", capturedAccount.Email);
            Assert.Equal("", capturedAccount.PhoneNumber);
            Assert.Equal("", capturedAccount.DialCode);
        }
    }
}
