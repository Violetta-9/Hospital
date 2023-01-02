﻿using Authorization.Data_Domain.Models;
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
    public class OfficeMappingConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.Property(x => x.Address).HasColumnName(nameof(Office.Address)).HasMaxLength(32)
                .IsRequired();
            builder.Property(x => x.RegistryPhoneNumber).HasColumnName(nameof(Office.RegistryPhoneNumber)).HasMaxLength(12)
                .IsRequired();
            builder.Property(x => x.IsActive).HasColumnName(nameof(Office.IsActive))
                .IsRequired();
            builder.HasMany(x => x.Doctors).WithOne(x => x.Office).HasForeignKey(x => x.OfficeId);
            builder.HasMany(x => x.Receptionists).WithOne(x => x.Office).HasForeignKey(x => x.OfficeId);
           

        }
    }
}