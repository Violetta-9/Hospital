﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.API.Application.Contracts.Incoming
{
    public class UpdateAppointmentResultDTO
    {
        public long AppointmentResultId { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }
    }
}
