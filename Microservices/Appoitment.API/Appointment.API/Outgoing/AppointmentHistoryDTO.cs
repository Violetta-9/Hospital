using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.API.Application.Contracts.Outgoing
{
    public class AppointmentHistoryDTO
    {
        
        public DateTime DateTime { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }
    }
}
