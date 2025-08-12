using MediatR;
using Microservice.Account.EFCore.EntityConfiguration;
using Microservice.Account.SharedKernel.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AccountEntity = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;

namespace Microservice.Account.EFCore.Context
{
    public class MicroserviceAccountWriteContext : DbContext, IUnitOfWork
    {
        public const string DefaultSchema = "public";
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public DbSet<AccountEntity> Account { get; set; }


        public MicroserviceAccountWriteContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.EnableDiscardEvents", false);
        }
        public MicroserviceAccountWriteContext(DbContextOptions<MicroserviceAccountWriteContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            System.Diagnostics.Debug.WriteLine("MicroserviceAccountWriteContext::ctor ->" + Guid.NewGuid());
        }
        public MicroserviceAccountWriteContext(DbContextOptions<MicroserviceAccountWriteContext> options, IMediator mediator,
       IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            _mediator = mediator;
            System.Diagnostics.Debug.WriteLine("BuybaseAdminWriteContext::ctor ->" + this.GetHashCode());
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.EnableDiscardEvents", false);
        }
        public MicroserviceAccountWriteContext(DbContextOptions<MicroserviceAccountWriteContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.EnableSensitiveDataLogging();
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
