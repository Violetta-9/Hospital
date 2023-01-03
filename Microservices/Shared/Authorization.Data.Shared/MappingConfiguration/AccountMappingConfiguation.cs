using Authorization.Data.Shared.MappingConfiguration.Extension;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Authorization.Data.Shared.MappingConfiguration
{
    public class AccountMappingConfiguation : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
           builder.MapAuditProperty();
            builder.Property(x => x.FirstName).HasColumnName(nameof(Account.FirstName)).HasMaxLength(256)
              .IsRequired();
            builder.Property(x => x.Birthday).HasColumnName(nameof(Account.Birthday)).HasMaxLength(36)
                .IsRequired();
            builder.Property(x => x.LastName).HasColumnName(nameof(Account.LastName)).HasMaxLength(256).IsRequired();
            builder.Property(x => x.MiddleName).HasColumnName(nameof(Account.MiddleName)).HasMaxLength(256).IsRequired();
            
            
        }
    }
}
