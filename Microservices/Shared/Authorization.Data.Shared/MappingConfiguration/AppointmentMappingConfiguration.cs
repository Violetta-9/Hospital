using Authorization.Data.Shared.MappingConfiguration.Extension;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Data.Shared.MappingConfiguration
{
    public class AppointmentMappingConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.BasedMapping();
            builder.Property(x => x.DateTime).HasColumnName(nameof(Appointment.DateTime)).HasMaxLength(12).IsRequired();
           
            builder.HasOne(x => x.Doctor).WithMany(x => x.Appointments).HasForeignKey(x => x.DoctorId);
            builder.HasOne(x => x.Service).WithMany(x => x.Appointments).HasForeignKey(x => x.ServiceId);
            builder.HasOne(x => x.Patient).WithMany(x => x.Appointments).HasForeignKey(x => x.PatientId);
            builder.HasOne(x => x.Office).WithMany(x => x.Appointments).HasForeignKey(x => x.OfficeId);
            builder.HasOne(x => x.Specialization).WithMany(x => x.Appointments).HasForeignKey(x => x.SpecializationId);
            builder.Property(x => x.IsApproved).HasColumnName(nameof(Appointment.IsApproved));

        }
    }
}
