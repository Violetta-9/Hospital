using Authorization.Data.Shared.MappingConfiguration.Extension;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Data.Shared.MappingConfiguration;

public class StatusMappingConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.BasedMapping();
        builder.Property(x => x.Title).HasColumnName(nameof(Status.Title)).HasMaxLength(32)
            .IsRequired();
        builder.HasMany(x => x.Doctors).WithOne(x => x.Status).HasForeignKey(x => x.StatusId);
    }
}