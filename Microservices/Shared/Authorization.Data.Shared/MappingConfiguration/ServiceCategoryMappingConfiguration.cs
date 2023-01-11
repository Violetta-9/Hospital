using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Authorization.Data.Shared.MappingConfiguration.Extension;

namespace Authorization.Data.Shared.MappingConfiguration
{
    public class ServiceCategoryMappingConfiguration : IEntityTypeConfiguration<ServiceCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceCategory> builder)
        {
            builder.BasedMapping();
            builder.Property(x=>x.Title).HasColumnName(nameof(ServiceCategory.Title)).HasMaxLength(12)
                .IsRequired();
            builder.Property(x => x.TimeSlotSize).HasColumnName(nameof(ServiceCategory.TimeSlotSize)).IsRequired();
            builder.HasMany(x => x.Services).WithOne(x => x.ServiceCategory).HasForeignKey(x => x.ServiceCategoryId);
        }
    }
}
