using Microservice.Account.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AccountModel = Microservice.Account.Domain.AggregateModels.AccountAggregate.AccountEntity.Account;

namespace Microservice.Account.EFCore.EntityConfiguration
{
    public class AccountEntityConfiguration : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure(EntityTypeBuilder<AccountModel> builder)
        {
            builder.ToTable("Account", MicroserviceAccountWriteContext.DefaultSchema);
            builder.HasKey(u => u.Id);
            builder.Property(b => b.Id).HasColumnType("UUID").ValueGeneratedNever().IsRequired();

            builder.HasQueryFilter(qf => qf.DeletedOn == null);
        }
    }
}
