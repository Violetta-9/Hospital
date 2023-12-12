
namespace Appointment.API.Application.Contracts.Incoming
{
    public class CreateAppointmentResultDto
    {
        public long AppointmentId { get; set; }
        public long PatientId { get; set; }
        public string PatientFullName { get; set; }
        public string ServiceTitle { get; set; }
        public string DoctorFullName { get; set; }
        public string SpecializationTitle { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendation { get; set; }
    }
}
