using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.API.Application.Contracts.Outgoing
{
    public class AppointmentScheduleForDoctorDTO
    {
        public long AppointmentId { get; set; }
        public DateTime DateTime { get; set; }
        public string PatientFullName { get; set; }
        public string ServiceName { get; set; }
        public bool ApprovedStatus { get; set; }
    }
}
