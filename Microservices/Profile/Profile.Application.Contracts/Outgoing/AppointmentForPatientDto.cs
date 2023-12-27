using Authorization.Data_Domain.Models;

namespace Profile.Application.Contracts.Outgoing
{
    public class AppointmentForPatientDto
    {
        public string Date { get; set; }
        public string DoctorFullName { get; set; }
        public List<ResultDto> Children { get; set; }
    }
}
