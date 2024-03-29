﻿
namespace Appointment.API.Application.Contracts.Outgoing
{
    public class AppointmentScheduleForReceptionistDTO
    {
        public long AppointmentId { get; set; }
        public DateTime DateTime { get; set; }
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string ServiceName { get; set; }
        public string SpecializationName { get; set; }
        public bool IsApprove { get; set; }
        public long PatientId { get; set; }

    }
}
