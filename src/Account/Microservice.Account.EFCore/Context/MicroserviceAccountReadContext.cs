using Microservice.Account.EFCore.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using AccountEntity = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;

namespace Microservice.Account.EFCore.Context
{
    public class MicroserviceAccountReadContext : DbContext
    {
        public const string DefaultSchema = "public";


        public DbSet<AccountEntity> Account { get; set; }

        public MicroserviceAccountReadContext(DbContextOptions<MicroserviceAccountReadContext> options) : base(options)
        {
        }

        public MicroserviceAccountReadContext()
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
            modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
