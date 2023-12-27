
namespace Appointment.API.Application.Contracts.Outgoing
{
    public class AppointmentScheduleForDoctorDTO
    {
        public long AppointmentId { get; set; }
        public DateTime DateTime { get; set; }
        public string PatientFullName { get; set; }
        public string ServiceName { get; set; }
        public string SpecializationName { get; set; }
        public bool ApprovedStatus { get; set; }
        public long? ResultId { get; set; }
        public long PatientId { get; set; }
    }
}
