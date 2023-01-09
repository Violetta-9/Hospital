using Authorization.Data.Shared.MappingConfiguration.Extension;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Data.Shared.MappingConfiguration;

public class DoctorMappingConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.BasedMapping();
        builder.Property(x => x.CareerStartYear).HasColumnName(nameof(Doctor.CareerStartYear)).HasMaxLength(12)
            .IsRequired();
        builder.HasOne(x => x.Specialization).WithMany(x => x.Doctors).HasForeignKey(x => x.SpecializationId);
        builder.HasOne(x => x.Office).WithMany(x => x.Doctors).HasForeignKey(x => x.OfficeId);
        builder.HasOne(x => x.Status).WithMany(x => x.Doctors).HasForeignKey(x => x.StatusId);
        builder.HasOne(x => x.Account).WithOne(x => x.Doctors).HasForeignKey<Doctor>(x => x.AccountId);
    }
}