using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Appointment.API.Application.Contracts.Incoming
{
    public class CreateAppointmentDTO
    {
        public long DoctorId { get; set; }
        public long PatientId { get; set; }
        public long ServiceId { get; set; }
        public long SpecializationId { get; set; }
        public long OfficeId { get; set; }
        public DateTime DateTime { get; set; }
     

    }
}