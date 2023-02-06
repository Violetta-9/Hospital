using Authorization.Data.Shared.MappingConfiguration.Extension;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Data.Shared.MappingConfiguration;

public class PhotoMappingConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.BasedMapping();
        builder.Property(x => x.Path).HasColumnName(nameof(Photo.Path)).HasMaxLength(512).IsRequired();
        builder.Property(x => x.FileName).HasColumnName(nameof(Photo.FileName)).HasMaxLength(264).IsRequired();
        builder.Property(x => x.ContainerName).HasColumnName(nameof(Photo.ContainerName)).HasMaxLength(100)
            .IsRequired();
        builder.HasOne(x => x.Account).WithOne(x => x.Photo).HasForeignKey<Account>(x => x.PhotoId);
        builder.HasOne(x => x.Office).WithOne(x => x.Photo).HasForeignKey<Office>(x => x.PhotoId);
    }
}