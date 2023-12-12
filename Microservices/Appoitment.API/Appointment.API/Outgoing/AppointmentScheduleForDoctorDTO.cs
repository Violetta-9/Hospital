﻿
namespace Appointment.API.Application.Contracts.Outgoing
{
    public class AppointmentScheduleForDoctorDTO
    {
        public long AppointmentId { get; set; }
        public DateTime DateTime { get; set; }
        public string PatientFullName { get; set; }
        public string ServiceName { get; set; }
        public bool ApprovedStatus { get; set; }
        public string PatientAccountId { get; set; }
        public string DoctorAccountId { get; set; }
    }
}
