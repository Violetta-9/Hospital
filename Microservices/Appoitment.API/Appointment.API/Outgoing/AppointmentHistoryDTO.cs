
namespace Appointment.API.Application.Contracts.Outgoing
{
    public class AppointmentHistoryDTO
    {
        public DateTime DateTime { get; set; }
        public string DoctorFullName { get; set; }
        public string ServiceName { get; set; }
        public bool IsApprove { get; set; }
        public long SpecializationId { get; set; }
        public long AppointmentId { get; set; }
    }
}
