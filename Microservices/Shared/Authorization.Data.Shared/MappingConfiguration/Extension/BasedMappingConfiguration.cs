using Authorization.Data_Domain.Models;
using Authorization.Data_Domain.Models.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Data.Shared.MappingConfiguration.Extension
{
    public static class BasedMappingConfiguration
    {
        public static void BasedMapping<T>(this EntityTypeBuilder<T> builder) where T : class,IAudientable, IIdentifiable<long>
        {
            builder.MapAuditProperty();
            builder.MapIdentifier();
        }
        public static void MapAuditProperty<T>(this EntityTypeBuilder<T> builder) where T : class,IAudientable
        {
            builder.Property(x => x.RowCreatedTimestamp).HasColumnName(nameof(Account.RowCreatedTimestamp)).IsRequired();
            builder.Property(x => x.LastRowModificationTimestamp).HasColumnName(nameof(Account.LastRowModificationTimestamp)).IsRequired();
        }

        public static void MapIdentifier<T>(this EntityTypeBuilder<T> builder) where T : class, IIdentifiable<long>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
