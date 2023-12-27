
namespace Appointment.API.Application.Contracts.Incoming
{
    public class RescheduleAppointmentDTO
    {
        public long AppointmentId { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
        public DateTime DataTime { get; set; }
   
    }
}
