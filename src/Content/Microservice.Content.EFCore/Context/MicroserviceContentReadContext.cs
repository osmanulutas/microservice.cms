using Microservice.Content.EFCore.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using ContentEntity = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.EFCore.Context
{
    public class MicroserviceContentReadContext : DbContext
    {
        public const string DefaultSchema = "public";


        public DbSet<ContentEntity> Content { get; set; }

        public MicroserviceContentReadContext(DbContextOptions<MicroserviceContentReadContext> options) : base(options)
        {
        }

        public MicroserviceContentReadContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.EnableDiscardEvents", false);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.EnableSensitiveDataLogging();
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContentConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
