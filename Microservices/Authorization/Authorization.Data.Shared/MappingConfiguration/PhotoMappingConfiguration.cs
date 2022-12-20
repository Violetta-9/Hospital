using Authorization.Data.Shared.MappingConfiguration.Extension;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Authorization.Data.Shared.MappingConfiguration
{
    public class PhotoMappingConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.BasedMapping();
            builder.Property(x => x.Url).HasColumnName(nameof(Photo.Url)).HasMaxLength(516).IsRequired();
        }
    }
}
