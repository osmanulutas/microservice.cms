using MediatR;
using Microservice.Content.EFCore.EntityConfiguration;
using Microservice.Content.SharedKernel.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ContentEntity = Microservice.Content.Domain.AggregateModels.ContentAggregate.ContentEntity.Content;

namespace Microservice.Content.EFCore.Context
{
    public class MicroserviceContentWriteContext : DbContext, IUnitOfWork
    {
        public const string DefaultSchema = "public";
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public DbSet<ContentEntity> Content { get; set; }


        public MicroserviceContentWriteContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.EnableDiscardEvents", false);
        }
        public MicroserviceContentWriteContext(DbContextOptions<MicroserviceContentWriteContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            System.Diagnostics.Debug.WriteLine("MicroserviceContentWriteContext::ctor ->" + Guid.NewGuid());
        }
        public MicroserviceContentWriteContext(DbContextOptions<MicroserviceContentWriteContext> options, IMediator mediator,
       IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            _mediator = mediator;
            System.Diagnostics.Debug.WriteLine("MicroserviceContentWriteContext::ctor ->" + this.GetHashCode());
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.EnableDiscardEvents", false);
        }
        public MicroserviceContentWriteContext(DbContextOptions<MicroserviceContentWriteContext> options) : base(options)
        {
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

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
