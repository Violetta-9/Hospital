using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Data.Shared.MappingConfiguration
{
    public class PatientMappingConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasOne(x => x.Account).WithOne(x => x.Patients).HasForeignKey<Patient>(x=>x.AccountId);
        }
    }
}
