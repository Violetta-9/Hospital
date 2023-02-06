using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Shared.MappingConfiguration.Extension;

namespace Authorization.Data.Shared.MappingConfiguration
{
    public class ResultMappingConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.BasedMapping();
            builder.Property(x => x.Complaints).HasColumnName(nameof(Result.Complaints)).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Conclusion).HasColumnName(nameof(Result.Conclusion)).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Recomendations).HasColumnName(nameof(Result.Recomendations)).HasMaxLength(250).IsRequired();
            builder.HasOne(x=>x.Appointment).WithOne(x=>x.Result).HasForeignKey<Result>(x=>x.AppointmentId);
            
        }
    }
}
