using ContentEntity = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;
using Xunit;

namespace Microservice.Content.Tests
{
    public class ContentTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateContent()
        {
            // Arrange
            var title = "Test Article";
            var description = "This is a test article";
            var body = "This is the body of the test article";
            var authorId = Guid.NewGuid();
            var category = "Technology";
            var tags = "test,article,technology";

            // Act
            var content = new ContentEntity(title, description, body, authorId, category, tags);

            // Assert
            Assert.NotEqual(Guid.Empty, content.Id);
            Assert.Equal(title, content.Title);
            Assert.Equal(description, content.Description);
            Assert.Equal(body, content.Body);
            Assert.Equal(authorId, content.AuthorId);
            Assert.Equal(category, content.Category);
            Assert.Equal(tags, content.Tags);
            Assert.False(content.IsPublished);
            Assert.Null(content.PublishedDate);
            Assert.True(content.CreatedOn <= DateTime.UtcNow);
            Assert.Null(content.UpdatedOn);
            Assert.Null(content.DeletedOn);
        }

        [Fact]
        public void Constructor_WithNullParameters_ShouldCreateContentWithNullValues()
        {
            // Arrange
            var authorId = Guid.NewGuid();

            // Act
            var content = new ContentEntity(null, null, null, authorId, null, null);

            // Assert
            Assert.NotEqual(Guid.Empty, content.Id);
            Assert.Null(content.Title);
            Assert.Null(content.Description);
            Assert.Null(content.Body);
            Assert.Equal(authorId, content.AuthorId);
            Assert.Null(content.Category);
            Assert.Null(content.Tags);
            Assert.False(content.IsPublished);
            Assert.Null(content.PublishedDate);
        }

        [Fact]
        public void DeleteContent_ShouldSetDeletedOnTimestamp()
        {
            // Arrange
            var content = new ContentEntity("Test Article", "Description", "Body", Guid.NewGuid(), "Category", "Tags");

            // Act
            content.DeleteContent();

            // Assert
            Assert.NotNull(content.DeletedOn);
            Assert.True(content.DeletedOn <= DateTime.UtcNow);
        }

        [Fact]
        public void Updated_ShouldSetUpdatedOnTimestamp()
        {
            // Arrange
            var content = new ContentEntity("Test Article", "Description", "Body", Guid.NewGuid(), "Category", "Tags");

            // Act
            content.Updated();

            // Assert
            Assert.NotNull(content.UpdatedOn);
            Assert.True(content.UpdatedOn <= DateTime.UtcNow);
        }

        [Fact]
        public void UpdateContent_WithValidParameters_ShouldUpdateFields()
        {
            // Arrange
            var content = new ContentEntity("Old Title", "Old Description", "Old Body", Guid.NewGuid(), "Old Category", "Old Tags");
            var newTitle = "New Title";
            var newDescription = "New Description";
            var newAuthorId = Guid.NewGuid();

            // Act
            content.UpdateContent(newAuthorId, newTitle, newDescription);

            // Assert
            Assert.Equal(newTitle, content.Title);
            Assert.Equal(newDescription, content.Description);
            Assert.Equal(newAuthorId, content.AuthorId);
            Assert.NotNull(content.UpdatedOn);
        }

        [Fact]
        public void UpdateContent_WithNullParameters_ShouldNotUpdateFields()
        {
            // Arrange
            var originalTitle = "Original Title";
            var originalDescription = "Original Description";
            var originalAuthorId = Guid.NewGuid();
            var content = new ContentEntity(originalTitle, originalDescription, "Body", originalAuthorId, "Category", "Tags");

            // Act
            content.UpdateContent(originalAuthorId, title: null, description: null);

            // Assert
            Assert.Equal(originalTitle, content.Title);
            Assert.Equal(originalDescription, content.Description);
        }

        [Fact]
        public void UpdateContent_ShouldCallUpdatedMethod()
        {
            // Arrange
            var content = new ContentEntity("Test Article", "Description", "Body", Guid.NewGuid(), "Category", "Tags");

            // Act
            content.UpdateContent(Guid.NewGuid(), title: "Updated Title");

            // Assert
            Assert.NotNull(content.UpdatedOn);
        }

        [Fact]
        public void UpdateContent_WithAllParameters_ShouldUpdateAllFields()
        {
            // Arrange
            var content = new ContentEntity("Old Title", "Old Description", "Old Body", Guid.NewGuid(), "Old Category", "Old Tags");
            var newTitle = "New Title";
            var newDescription = "New Description";
            var newBody = "New Body";
            var newCategory = "New Category";
            var newTags = "new,tags";
            var newAuthorId = Guid.NewGuid();

            // Act
            content.UpdateContent(newAuthorId, newTitle, newDescription, newBody, newCategory, newTags);

            // Assert
            Assert.Equal(newTitle, content.Title);
            Assert.Equal(newDescription, content.Description);
            Assert.Equal(newBody, content.Body);
            Assert.Equal(newCategory, content.Category);
            Assert.Equal(newTags, content.Tags);
            Assert.Equal(newAuthorId, content.AuthorId);
        }

        [Fact]
        public void Publish_ShouldSetIsPublishedToTrueAndSetPublishedDate()
        {
            // Arrange
            var content = new ContentEntity("Test Article", "Description", "Body", Guid.NewGuid(), "Category", "Tags");

            // Act
            content.Publish();

            // Assert
            Assert.True(content.IsPublished);
            Assert.NotNull(content.PublishedDate);
            Assert.True(content.PublishedDate <= DateTime.UtcNow);
            Assert.NotNull(content.UpdatedOn);
        }

        [Fact]
        public void Unpublish_ShouldSetIsPublishedToFalseAndClearPublishedDate()
        {
            // Arrange
            var content = new ContentEntity("Test Article", "Description", "Body", Guid.NewGuid(), "Category", "Tags");
            content.Publish();

            // Act
            content.Unpublish();

            // Assert
            Assert.False(content.IsPublished);
            Assert.Null(content.PublishedDate);
            Assert.NotNull(content.UpdatedOn);
        }

        [Fact]
        public void Publish_ShouldCallUpdatedMethod()
        {
            // Arrange
            var content = new ContentEntity("Test Article", "Description", "Body", Guid.NewGuid(), "Category", "Tags");

            // Act
            content.Publish();

            // Assert
            Assert.NotNull(content.UpdatedOn);
        }

        [Fact]
        public void Unpublish_ShouldCallUpdatedMethod()
        {
            // Arrange
            var content = new ContentEntity("Test Article", "Description", "Body", Guid.NewGuid(), "Category", "Tags");
            content.Publish();

            // Act
            content.Unpublish();

            // Assert
            Assert.NotNull(content.UpdatedOn);
        }

        [Fact]
        public void UpdateContent_WithPartialParameters_ShouldUpdateOnlySpecifiedFields()
        {
            // Arrange
            var content = new ContentEntity("Old Title", "Old Description", "Old Body", Guid.NewGuid(), "Old Category", "Old Tags");
            var newTitle = "New Title";
            var newAuthorId = Guid.NewGuid();

            // Act
            content.UpdateContent(newAuthorId, title: newTitle);

            // Assert
            Assert.Equal(newTitle, content.Title);
            Assert.Equal("Old Description", content.Description);
            Assert.Equal("Old Body", content.Body);
            Assert.Equal("Old Category", content.Category);
            Assert.Equal("Old Tags", content.Tags);
            Assert.Equal(newAuthorId, content.AuthorId);
        }
    }
}
