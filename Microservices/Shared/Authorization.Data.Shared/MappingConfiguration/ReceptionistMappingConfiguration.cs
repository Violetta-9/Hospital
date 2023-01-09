using Authorization.Data.Shared.MappingConfiguration.Extension;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Data.Shared.MappingConfiguration;

public class ReceptionistMappingConfiguration : IEntityTypeConfiguration<Receptionist>
{
    public void Configure(EntityTypeBuilder<Receptionist> builder)
    {
        builder.BasedMapping();
        builder.HasOne(x => x.Account).WithOne(x => x.Receptionists).HasForeignKey<Patient>(x => x.AccountId);
        builder.HasOne(x => x.Office).WithMany(x => x.Receptionists).HasForeignKey(x => x.OfficeId);
    }
}