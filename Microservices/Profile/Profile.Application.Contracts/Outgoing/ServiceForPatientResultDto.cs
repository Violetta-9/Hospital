

using Authorization.Data_Domain.Models;

namespace Profile.Application.Contracts.Outgoing
{
    public class ServiceForPatientResultDto
    {
        public string Title { get; set; }
        public List<AppointmentForPatientDto> Children { get; set; }
    }
}
