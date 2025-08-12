using Microservice.Content.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ContentEntity = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.EFCore.EntityConfiguration
{
    public class ContentConfiguration : IEntityTypeConfiguration<ContentEntity>
    {
        public void Configure(EntityTypeBuilder<ContentEntity> builder)
        {
            builder.ToTable("Contents", MicroserviceContentWriteContext.DefaultSchema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.Body)
                .IsRequired(false);

            builder.Property(x => x.Category)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.Tags)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.IsPublished)
                .IsRequired();

            builder.Property(x => x.PublishedDate)
                .IsRequired(false);

            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.Property(x => x.UpdatedOn)
                .IsRequired(false);

            builder.Property(x => x.DeletedOn)
                .IsRequired(false);

            builder.HasQueryFilter(x => x.DeletedOn == null);
        }
    }
}
