using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Shared.MappingConfiguration.Extension;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Data.Shared.MappingConfiguration
{
    public class SpecializationMappingConfiguration: IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.BasedMapping();
            builder.Property(x => x.Title).HasColumnName(nameof(Specialization.Title)).HasMaxLength(32)
                .IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(nameof(Specialization.IsActive)).IsRequired();
            builder.HasMany(x => x.Doctors).WithOne(x => x.Specialization).HasForeignKey(x => x.SpecializationId);
        }
    }
}
