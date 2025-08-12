using MediatR;
using Microservice.Content.Application.Content.Commands.AddContent;
using ContentEntity = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;
using Microservice.Content.SharedKernel.Model;
using Microservice.Content.SharedKernel.SeedWork;
using Moq;
using System.Net;
using Xunit;

namespace Microservice.Content.Tests
{
    public class AddContentCommandHandlerTests
    {
        private readonly Mock<IWriteRepository<ContentEntity>> _mockRepository;
        private readonly AddContentCommandHandler _handler;

        public AddContentCommandHandlerTests()
        {
            _mockRepository = new Mock<IWriteRepository<ContentEntity>>();
            _handler = new AddContentCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_WithValidCommand_ShouldReturnSuccessResponse()
        {
            // Arrange
            var command = new AddContentCommand
            {
                Title = "Test Article",
                Description = "This is a test article",
                Body = "This is the body of the test article",
                AuthorId = Guid.NewGuid(),
                Category = "Technology",
                Tags = "test,article,technology"
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContentEntity("Test", "Test", "Test", Guid.NewGuid(), "Test", "Test"));
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangeAsync(It.IsAny<CancellationToken>()))
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
            var command = new AddContentCommand
            {
                Title = "Test Article",
                Description = "This is a test article",
                Body = "This is the body of the test article",
                AuthorId = Guid.NewGuid(),
                Category = "Technology",
                Tags = "test,article,technology"
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContentEntity("Test", "Test", "Test", Guid.NewGuid(), "Test", "Test"));
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangeAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Data Save Error", result.Title);
            Assert.Equal("Content Cant Save", result.Detail);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.Status);
        }

        [Fact]
        public async Task Handle_ShouldCreateContentWithCorrectParameters()
        {
            // Arrange
            var command = new AddContentCommand
            {
                Title = "New Article",
                Description = "New article description",
                Body = "New article body content",
                AuthorId = Guid.NewGuid(),
                Category = "Science",
                Tags = "science,research,new"
            };

            ContentEntity capturedContent = null;
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .Callback<ContentEntity, CancellationToken>((content, token) => capturedContent = content)
                .ReturnsAsync(new ContentEntity("Test", "Test", "Test", Guid.NewGuid(), "Test", "Test"));
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangeAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(capturedContent);
            Assert.Equal(command.Title, capturedContent.Title);
            Assert.Equal(command.Description, capturedContent.Description);
            Assert.Equal(command.Body, capturedContent.Body);
            Assert.Equal(command.AuthorId, capturedContent.AuthorId);
            Assert.Equal(command.Category, capturedContent.Category);
            Assert.Equal(command.Tags, capturedContent.Tags);
            Assert.NotEqual(Guid.Empty, capturedContent.Id);
            Assert.False(capturedContent.IsPublished);
            Assert.Null(capturedContent.PublishedDate);
            Assert.True(capturedContent.CreatedOn <= DateTime.UtcNow);
        }

        [Fact]
        public async Task Handle_ShouldCallRepositoryMethodsInCorrectOrder()
        {
            // Arrange
            var command = new AddContentCommand
            {
                Title = "Test Article",
                Description = "Description",
                Body = "Body",
                AuthorId = Guid.NewGuid(),
                Category = "Category",
                Tags = "Tags"
            };

            var callOrder = new List<string>();
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("AddAsync"))
                .ReturnsAsync(new ContentEntity("Test", "Test", "Test", Guid.NewGuid(), "Test", "Test"));
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangeAsync(It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("SaveChangeAsync"))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(2, callOrder.Count);
            Assert.Equal("AddAsync", callOrder[0]);
            Assert.Equal("SaveChangeAsync", callOrder[1]);
        }

        [Fact]
        public async Task Handle_WithNullValues_ShouldCreateContentWithNullValues()
        {
            // Arrange
            var command = new AddContentCommand
            {
                Title = null,
                Description = null,
                Body = null,
                AuthorId = Guid.NewGuid(),
                Category = null,
                Tags = null
            };

            ContentEntity capturedContent = null;
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .Callback<ContentEntity, CancellationToken>((content, token) => capturedContent = content)
                .ReturnsAsync(new ContentEntity("Test", "Test", "Test", Guid.NewGuid(), "Test", "Test"));
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangeAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(capturedContent);
            Assert.Null(capturedContent.Title);
            Assert.Null(capturedContent.Description);
            Assert.Null(capturedContent.Body);
            Assert.Equal(command.AuthorId, capturedContent.AuthorId);
            Assert.Null(capturedContent.Category);
            Assert.Null(capturedContent.Tags);
        }

        [Fact]
        public async Task Handle_WithEmptyStrings_ShouldCreateContentWithEmptyStrings()
        {
            // Arrange
            var command = new AddContentCommand
            {
                Title = "",
                Description = "",
                Body = "",
                AuthorId = Guid.NewGuid(),
                Category = "",
                Tags = ""
            };

            ContentEntity capturedContent = null;
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .Callback<ContentEntity, CancellationToken>((content, token) => capturedContent = content)
                .ReturnsAsync(new ContentEntity("Test", "Test", "Test", Guid.NewGuid(), "Test", "Test"));
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangeAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(capturedContent);
            Assert.Equal("", capturedContent.Title);
            Assert.Equal("", capturedContent.Description);
            Assert.Equal("", capturedContent.Body);
            Assert.Equal("", capturedContent.Category);
            Assert.Equal("", capturedContent.Tags);
        }

        [Fact]
        public async Task Handle_ShouldSetDefaultValuesCorrectly()
        {
            // Arrange
            var command = new AddContentCommand
            {
                Title = "Test Article",
                Description = "Description",
                Body = "Body",
                AuthorId = Guid.NewGuid(),
                Category = "Category",
                Tags = "Tags"
            };

            ContentEntity capturedContent = null;
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .Callback<ContentEntity, CancellationToken>((content, token) => capturedContent = content)
                .ReturnsAsync(new ContentEntity("Test", "Test", "Test", Guid.NewGuid(), "Test", "Test"));
            _mockRepository.Setup(r => r.UnitOfWork.SaveChangeAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(capturedContent);
            Assert.False(capturedContent.IsPublished);
            Assert.Null(capturedContent.PublishedDate);
            Assert.True(capturedContent.CreatedOn <= DateTime.UtcNow);
            Assert.Null(capturedContent.UpdatedOn);
            Assert.Null(capturedContent.DeletedOn);
        }
    }
}
