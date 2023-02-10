using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.API.Application.Contracts.Incoming
{
    public class RescheduleAppointmentDTO
    {
        public long AppointmentId { get; set; }
        public long DoctorId { get; set; }
        public DateTime DataTime { get; set; }
   
    }
}
