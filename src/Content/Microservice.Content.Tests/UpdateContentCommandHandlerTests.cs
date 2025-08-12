using MediatR;
using Microservice.Content.Application.Content.Commands.UpdateContent;
using ContentEntity = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;
using Microservice.Content.SharedKernel.Model;
using Microservice.Content.SharedKernel.SeedWork;
using Moq;
using System.Net;
using Xunit;

namespace Microservice.Content.Tests
{
    public class UpdateContentCommandHandlerTests
    {
        private readonly Mock<IWriteRepository<ContentEntity>> _mockRepository;
        private readonly UpdateContentCommandHandler _handler;

        public UpdateContentCommandHandlerTests()
        {
            _mockRepository = new Mock<IWriteRepository<ContentEntity>>();
            _handler = new UpdateContentCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_WithValidCommand_ShouldReturnSuccessResponse()
        {
            // Arrange
            var contentId = Guid.NewGuid();
            var authorId = Guid.NewGuid();
            var existingContent = new ContentEntity("Old Title", "Old Description", "Old Body", authorId, "Old Category", "Old Tags");
            
            var command = new UpdateContentCommand
            {
                Id = contentId,
                Title = "New Title",
                Description = "New Description",
                Body = "New Body",
                AuthorId = authorId,
                Category = "New Category",
                Tags = "new,tags"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateContentSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingContent);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
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
        public async Task Handle_WhenContentNotFound_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var contentId = Guid.NewGuid();
            var command = new UpdateContentCommand
            {
                Id = contentId,
                Title = "New Title",
                Description = "New Description",
                Body = "New Body",
                AuthorId = Guid.NewGuid(),
                Category = "New Category",
                Tags = "new,tags"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateContentSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ContentEntity)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Not Found", result.Title);
            Assert.Equal("Content not found", result.Detail);
            Assert.Equal((int)HttpStatusCode.NotFound, result.Status);
        }

        [Fact]
        public async Task Handle_WhenSaveFails_ShouldReturnErrorResponse()
        {
            // Arrange
            var contentId = Guid.NewGuid();
            var authorId = Guid.NewGuid();
            var existingContent = new ContentEntity("Old Title", "Old Description", "Old Body", authorId, "Old Category", "Old Tags");
            
            var command = new UpdateContentCommand
            {
                Id = contentId,
                Title = "New Title",
                Description = "New Description",
                Body = "New Body",
                AuthorId = authorId,
                Category = "New Category",
                Tags = "new,tags"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateContentSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingContent);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Data Update Error", result.Title);
            Assert.Equal("Content Cant Update", result.Detail);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.Status);
        }

        [Fact]
        public async Task Handle_ShouldCallRepositoryMethodsInCorrectOrder()
        {
            // Arrange
            var contentId = Guid.NewGuid();
            var authorId = Guid.NewGuid();
            var existingContent = new ContentEntity("Old Title", "Old Description", "Old Body", authorId, "Old Category", "Old Tags");
            
            var command = new UpdateContentCommand
            {
                Id = contentId,
                Title = "New Title",
                Description = "New Description",
                Body = "New Body",
                AuthorId = authorId,
                Category = "New Category",
                Tags = "new,tags"
            };

            var callOrder = new List<string>();
            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateContentSpecification>(), It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("FirstOrDefaultAsync"))
                .ReturnsAsync(existingContent);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("UpdateAsync"))
                .ReturnsAsync(1);
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
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
        public async Task Handle_ShouldUpdateContentWithCorrectParameters()
        {
            // Arrange
            var contentId = Guid.NewGuid();
            var authorId = Guid.NewGuid();
            var existingContent = new ContentEntity("Old Title", "Old Description", "Old Body", authorId, "Old Category", "Old Tags");
            
            var command = new UpdateContentCommand
            {
                Id = contentId,
                Title = "New Title",
                Description = "New Description",
                Body = "New Body",
                AuthorId = authorId,
                Category = "New Category",
                Tags = "new,tags"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateContentSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingContent);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(command.Title, existingContent.Title);
            Assert.Equal(command.Description, existingContent.Description);
            Assert.Equal(command.Body, existingContent.Body);
            Assert.Equal(command.AuthorId, existingContent.AuthorId);
            Assert.Equal(command.Category, existingContent.Category);
            Assert.Equal(command.Tags, existingContent.Tags);
            Assert.NotNull(existingContent.UpdatedOn);
        }

        [Fact]
        public async Task Handle_WithNullValues_ShouldUpdateOnlySpecifiedFields()
        {
            // Arrange
            var contentId = Guid.NewGuid();
            var authorId = Guid.NewGuid();
            var existingContent = new ContentEntity("Old Title", "Old Description", "Old Body", authorId, "Old Category", "Old Tags");
            var originalTitle = existingContent.Title;
            var originalDescription = existingContent.Description;
            
            var command = new UpdateContentCommand
            {
                Id = contentId,
                Title = null,
                Description = null,
                Body = "New Body",
                AuthorId = authorId,
                Category = "New Category",
                Tags = "new,tags"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateContentSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingContent);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(originalTitle, existingContent.Title); // Should remain unchanged
            Assert.Equal(originalDescription, existingContent.Description); // Should remain unchanged
            Assert.Equal("New Body", existingContent.Body); // Should be updated
            Assert.Equal("New Category", existingContent.Category); // Should be updated
            Assert.Equal("new,tags", existingContent.Tags); // Should be updated
        }

        [Fact]
        public async Task Handle_WithEmptyStrings_ShouldUpdateFieldsWithEmptyStrings()
        {
            // Arrange
            var contentId = Guid.NewGuid();
            var authorId = Guid.NewGuid();
            var existingContent = new ContentEntity("Old Title", "Old Description", "Old Body", authorId, "Old Category", "Old Tags");
            
            var command = new UpdateContentCommand
            {
                Id = contentId,
                Title = "",
                Description = "",
                Body = "",
                AuthorId = authorId,
                Category = "",
                Tags = ""
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateContentSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingContent);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("", existingContent.Title);
            Assert.Equal("", existingContent.Description);
            Assert.Equal("", existingContent.Body);
            Assert.Equal("", existingContent.Category);
            Assert.Equal("", existingContent.Tags);
        }

        [Fact]
        public async Task Handle_WithNewAuthorId_ShouldUpdateAuthorId()
        {
            // Arrange
            var contentId = Guid.NewGuid();
            var oldAuthorId = Guid.NewGuid();
            var newAuthorId = Guid.NewGuid();
            var existingContent = new ContentEntity("Old Title", "Old Description", "Old Body", oldAuthorId, "Old Category", "Old Tags");
            
            var command = new UpdateContentCommand
            {
                Id = contentId,
                Title = "New Title",
                Description = "New Description",
                Body = "New Body",
                AuthorId = newAuthorId,
                Category = "New Category",
                Tags = "new,tags"
            };

            _mockRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<UpdateContentSpecification>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingContent);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<ContentEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _mockRepository.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(newAuthorId, existingContent.AuthorId);
        }
    }
}
