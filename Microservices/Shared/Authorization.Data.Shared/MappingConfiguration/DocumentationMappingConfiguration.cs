using Authorization.Data.Shared.MappingConfiguration.Extension;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Data.Shared.MappingConfiguration;

public class DocumentationMappingConfiguration : IEntityTypeConfiguration<Documentation>
{
    public void Configure(EntityTypeBuilder<Documentation> builder)
    {
        builder.BasedMapping();
        builder.Property(x => x.Path).HasColumnName(nameof(Documentation.Path)).HasMaxLength(512).IsRequired();
        builder.Property(x => x.FileName).HasColumnName(nameof(Documentation.FileName)).HasMaxLength(264).IsRequired();
        builder.Property(x => x.ContainerName).HasColumnName(nameof(Documentation.ContainerName)).HasMaxLength(100)
            .IsRequired();
        builder.HasOne(x => x.Account).WithOne(x => x.Documentation).HasForeignKey<Account>(x => x.DocumentationId);
        builder.HasOne(x => x.Office).WithOne(x => x.Documentation).HasForeignKey<Office>(x => x.DocumentationId);
    }
}