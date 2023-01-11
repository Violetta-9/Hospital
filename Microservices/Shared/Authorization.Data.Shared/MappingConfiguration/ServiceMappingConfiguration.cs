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
    public class ServiceMappingConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.BasedMapping();
            builder.Property(x => x.Title).HasColumnName(nameof(Service.Title)).HasMaxLength(12).IsRequired();
            builder.Property(x => x.Price).HasColumnName(nameof(Service.Price)).IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(nameof(Service.IsActive)).IsRequired();
            builder.HasOne(x => x.Specialization).WithMany(x=>x.Services).HasForeignKey(x=>x.SpecializationId);
            builder.HasOne(x => x.ServiceCategory).WithMany(x => x.Services).HasForeignKey(x => x.ServiceCategoryId);
        }
    }
}
