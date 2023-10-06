using Authorization.Data.Shared.MappingConfiguration.Extension;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Data.Shared.MappingConfiguration;

public class DocumentMappingConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.BasedMapping();
        builder.Property(x => x.Path).HasColumnName(nameof(Document.Path)).HasMaxLength(512).IsRequired();
        builder.Property(x => x.FileName).HasColumnName(nameof(Document.FileName)).HasMaxLength(264).IsRequired();
        builder.Property(x => x.ContainerName).HasColumnName(nameof(Document.ContainerName)).HasMaxLength(100)
            .IsRequired();
        builder.HasOne(x => x.Result).WithOne(x => x.Document).HasForeignKey<Document>(x => x.ResultId);
    }
}